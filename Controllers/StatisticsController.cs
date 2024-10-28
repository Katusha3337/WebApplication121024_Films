using Microsoft.AspNetCore.Mvc;
using WebApplication121024_Films.Data;
using WebApplication121024_Films.Models;

namespace WebApplication121024_Films.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly ApplicationContext _context;

        public StatisticsController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var totalMovies = _context.TvShow.Count();
            var averageRating = _context.TvShow.Any() ? (double)_context.TvShow.Average(m => m.Rating) : 0;
            var topGenre = _context.TvShow.GroupBy(m => m.Genre)
                                           .OrderByDescending(g => g.Count())
                                           .Select(g => g.Key.ToString())
                                           .FirstOrDefault() ?? "N/A";

            var stats = new MovieStatistics
            {
                TotalMovies = totalMovies,
                AverageRating = averageRating,
                TopGenre = topGenre
            };

            return View(stats);
        }
    }
}
