namespace WebApplication121024_Films.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int TvShowId { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public TvShow? TvShow { get; set; }
    }
}
