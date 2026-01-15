namespace LectorASPNET.Models
{
    public enum ReadStatus
    {
        WantToRead,
        Reading,
        Read,
        Abandoned
    }

    public class UserLibrary
    {
        public int Id { get; set; }

        public string BookId { get; set; } = string.Empty;
        public string BookTitle { get; set; } = string.Empty;
        public string? BookCoverUrl { get; set; }

        public ReadStatus Status { get; set; }
        public DateTime UpdateDate { get; set; } = DateTime.UtcNow;

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
