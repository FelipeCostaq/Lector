namespace LectorASPNET.Models
{
    public class Review
    {
        public int Id
        {
            get; set;
        }

        // Dados do Livro (Cache para evitar chamar API toda hora)
        public string LivroIdApi
        {
            get; set;
        } // ID do Google Books
        public string TituloLivro
        {
            get; set;
        }
        public string? CapaUrl
        {
            get; set;
        }

        // A avaliação
        public double Nota
        {
            get; set;
        } // Double aceita 4.5, 3.8, etc.
        public string? TextoResenha
        {
            get; set;
        } // Pode ser nulo se ele só der estrelas

        public DateTime DataPublicacao { get; set; } = DateTime.UtcNow;

        // Dono da avaliação
        public Guid UsuarioId
        {
            get; set;
        }
        public Usuario Usuario
        {
            get; set;
        }
    }
}
