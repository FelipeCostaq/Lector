using LectorASPNET.Data;
using LectorASPNET.DTO;
using LectorASPNET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LectorASPNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly Supabase.Client _supabaseClient;

        public AuthController(AppDbContext context, Supabase.Client supabaseClient)
        {
            _context = context;
            _supabaseClient = supabaseClient;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                return BadRequest("Email e senha são obrigatórios.");

            try
            {
                var session = await _supabaseClient.Auth.SignUp(request.Email, request.Password);

                if (session?.User == null)
                {
                    return BadRequest("Erro ao criar usuário no Supabase.");
                }

                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.AuthId == session.User.Id);

                if (existingUser == null)
                {
                    var newUser = new User
                    {
                        Id = Guid.NewGuid(),
                        AuthId = session.User.Id, 
                        Name = request.Username,
                        AvatarUrl = null,
                        Bio = "",

                        Review = new List<Review>(),
                        MyBooks = new List<UserLibrary>(),
                        Following = new List<Social>(),
                        Followers = new List<Social>()
                    };

                    _context.Users.Add(newUser);
                    await _context.SaveChangesAsync();
                }

                if (session.AccessToken == null)
                {
                    return Ok(new
                    {
                        message = "Cadastro realizado! Verifique seu email para ativar a conta."
                    });
                }

                return Ok(new
                {
                    token = session.AccessToken,
                    userId = session.User.Id,
                    internalId = existingUser?.Id, 
                    message = "Usuário cadastrado e logado com sucesso!"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    error = ex.Message
                });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            try
            {
                var session = await _supabaseClient.Auth.SignIn(request.Email, request.Password);

                if (session?.User == null || session.AccessToken == null)
                {
                    return Unauthorized("Login falhou. Verifique suas credenciais.");
                }

                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.AuthId == session.User.Id);

                Guid localUserId;

                if (existingUser == null)
                {
                    var newUser = new User
                    {
                        Id = Guid.NewGuid(),
                        AuthId = session.User.Id,
                        Name = "",
                        AvatarUrl = null,
                        Bio = "",
                        Review = new List<Review>(),
                        MyBooks = new List<UserLibrary>(),
                        Following = new List<Social>(),
                        Followers = new List<Social>()
                    };

                    _context.Users.Add(newUser);
                    await _context.SaveChangesAsync();
                    localUserId = newUser.Id;
                }
                else
                {
                    localUserId = existingUser.Id;
                }

                return Ok(new
                {
                    token = session.AccessToken,
                    userId = session.User.Id,
                    internalId = localUserId,
                    message = "Login realizado com sucesso!"
                });
            }
            catch (Exception ex)
            {
                return Unauthorized(new
                {    error = ex.Message
                });
            }
        }
    }
}