namespace LectorASPNET.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string BookId { get; set; } = string.Empty;
        public string BookTitle { get; set; } = string.Empty;
        public string? BookCoverUrl { get; set; }

        public double Rating { get; set; } 
        public string? ReviewText { get; set; } 
    
        public DateTime PublicationDate { get; set; } = DateTime.UtcNow;

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
