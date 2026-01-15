namespace LectorASPNET.Models
{
    public class Social
    {
        public Guid FollowerId { get; set; }
        public User Follower { get; set; }

        public Guid FollowingId { get; set; }
        public User Following { get; set; } 

        public DateTime FollowDate { get; set; } = DateTime.UtcNow;
    }
}
