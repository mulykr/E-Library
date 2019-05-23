namespace LiBook.Data.Entities
{
    public class CommentLike
    {
        public string Id { get; set; }
        public string UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public string CommentId { get; set; }
        public Comment Comment { get; set; }
        public bool Liked { get; set; }
    }
}
