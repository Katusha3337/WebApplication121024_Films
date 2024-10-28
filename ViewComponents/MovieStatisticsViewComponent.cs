using Microsoft.AspNetCore.Mvc;
using WebApplication121024_Films.Data;
using WebApplication121024_Films.Models;

namespace WebApplication121024_Films.ViewComponents
{
    namespace WebApplication121024_Films.ViewComponents
    {
        public class MovieStatisticsViewComponent : ViewComponent
        {
            private readonly ApplicationContext _context;
            public MovieStatisticsViewComponent(ApplicationContext context)
            {
                _context = context;
            }

            public IViewComponentResult Invoke()
            {
                var totalMovies = _context.TvShow.Count();
                var averageRating = _context.TvShow.Any() ? (double)_context.TvShow.Average(m => m.Rating) : 0;
                var topGenre = _context.TvShow.GroupBy(m => m.Genre)
                                               .OrderByDescending(g => g.Count())
                                               .Select(g => g.Key.ToString())
                                               .FirstOrDefault() ?? "N/A";
                var genres = _context.TvShow.GroupBy(m => m.Genre)
                                            .Select(g => new GenreStatistics
                                            {
                                                Genre = g.Key.ToString(),
                                                Count = g.Count()
                                            })
                                            .ToList();
                var stats = new MovieStatistics
                {
                    TotalMovies = totalMovies,
                    AverageRating = averageRating,
                    TopGenre = topGenre,
                    Genres = genres
                };

                return View(stats);
            }
        }
    }
}
