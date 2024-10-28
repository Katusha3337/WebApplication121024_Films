namespace WebApplication121024_Films.Models
{
    public class MovieStatistics
    {
        public int TotalMovies { get; set; }
        public double AverageRating { get; set; }
        public string TopGenre { get; set; }
        public List<GenreStatistics> Genres { get; set; }
    }    
}
