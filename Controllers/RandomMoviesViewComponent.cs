using Microsoft.AspNetCore.Mvc;
using WebApplication121024_Films.Data;
using WebApplication121024_Films.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication121024_Films.Controllers
{
    public class RandomMoviesViewComponent : ViewComponent
    {
        private readonly ApplicationContext _context;
        public RandomMoviesViewComponent(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync(int genreId)
        {
            var randomMovies = await _context.TvShow
                                             .Where(m => m.Genre == (Genre)genreId)
                                             .OrderBy(r => Guid.NewGuid())
                                             .Take(5)
                                             .ToListAsync();
            return View(randomMovies);
        }
    }
}
