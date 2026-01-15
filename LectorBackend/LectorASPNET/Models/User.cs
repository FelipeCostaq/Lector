namespace LectorASPNET.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string AuthId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public string? AvatarUrl { get; set; }

        public ICollection<Review> Review { get; set; } = new List<Review>();
        public ICollection<UserLibrary> MyBooks { get; set; } = new List<UserLibrary>();
        public ICollection<Social> Following { get; set; } = new List<Social>();
        public ICollection<Social> Followers { get; set; } = new List<Social>();

    }
}
