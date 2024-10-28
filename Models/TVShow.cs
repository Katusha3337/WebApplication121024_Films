using System.ComponentModel.DataAnnotations;

namespace WebApplication121024_Films.Models
{
    public enum Genre
    {
        Drama,
        Comedy,
        Romance,
        [Display(Name = "Romantic Comedy")]
        RomCom,
        Crime,
        Mystery
    }

    //public class TvShow
    //{
    //    public int Id { get; set; }
    //    public string Title { get; set; }
    //    public Genre Genre { get; set; }
    //    public decimal Rating {  get; set; }
    //    public string ImdbUrl { get; set; }
    //}

    public class TvShow
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter movie 'Title'")]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }
        public Genre Genre { get; set; }

        [Required(ErrorMessage = "Enter movie 'Rating'")]
        public decimal Rating { get; set; }

        [Required(ErrorMessage = "Enter movie 'Imdb Url'")]
        [DataType(DataType.Url)]
        [Display(Name = "Imdb Url")]
        public string ImdbUrl { get; set; }

        [Required(ErrorMessage = "Enter movie 'Poster'")]
        [DataType(DataType.Url)]
        [Display(Name = "Poster")]
        public string? ImageUrl { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
