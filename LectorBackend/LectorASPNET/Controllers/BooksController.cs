using Microsoft.AspNetCore.Mvc;
using LectorASPNET.Services;
using Microsoft.AspNetCore.Authorization;

namespace LectorASPNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly GoogleBooksService _googleBooksService;

        public BooksController(GoogleBooksService googleBooksService)
        {
            _googleBooksService = googleBooksService;
        }

        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("Por favor, digite o nome de um livro ou autor.");
            }

            var books = await _googleBooksService.SearchBooksAsync(query);

            return Ok(books);
        }
    }
}