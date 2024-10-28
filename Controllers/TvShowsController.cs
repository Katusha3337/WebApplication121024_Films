using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication121024_Films.Data;
using WebApplication121024_Films.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication121024_Films.Controllers
{
    public class TvShowsController : Controller
    {
        private readonly ApplicationContext _context;

        public TvShowsController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["RatingSortParm"] = sortOrder == "Rating" ? "rating_desc" : "Rating";
            ViewData["CurrentFilter"] = searchString;

            var tvShows = from s in _context.TvShow select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                tvShows = tvShows.Where(s => s.Title.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "title_desc":
                    tvShows = tvShows.OrderByDescending(s => s.Title);
                    break;
                case "Rating":
                    tvShows = tvShows.OrderBy(s => s.Rating);
                    break;
                case "rating_desc":
                    tvShows = tvShows.OrderByDescending(s => s.Rating);
                    break;
                default:
                    tvShows = tvShows.OrderBy(s => s.Title);
                    break;
            }

            int pageSize = 3;
            return View(await PaginatedList<TvShow>.CreateAsync(tvShows.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        // GET: TvShows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tvShow = await _context.TvShow
                .Include(t => t.Comments)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tvShow == null)
            {
                return NotFound();
            }

            return View(tvShow);
        }

        // GET: TvShows/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TvShows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Genre,Rating,ImdbUrl,ImageUrl")] TvShow tvShow)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tvShow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tvShow);
        }

        // GET: TvShows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tvShow = await _context.TvShow.FindAsync(id);
            if (tvShow == null)
            {
                return NotFound();
            }
            return View(tvShow);
        }

        // POST: TvShows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre,Rating,ImdbUrl,ImageUrl")] TvShow tvShow)
        {
            if (id != tvShow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tvShow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TvShowExists(tvShow.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tvShow);
        }

        // GET: TvShows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tvShow = await _context.TvShow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tvShow == null)
            {
                return NotFound();
            }

            return View(tvShow);
        }

        // POST: TvShows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tvShow = await _context.TvShow.FindAsync(id);
            if (tvShow != null)
            {
                _context.TvShow.Remove(tvShow);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TvShowExists(int id)
        {
            return _context.TvShow.Any(e => e.Id == id);
        }
        public async Task<IActionResult> Genre(string genre, int? pageNumber)
        {
            ViewData["CurrentGenre"] = genre;

            var tvShows = from s in _context.TvShow where s.Genre.ToString() == genre select s;

            int pageSize = 3;
            return View("Index", await PaginatedList<TvShow>.CreateAsync(tvShows.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
        [HttpPost]
        public async Task<IActionResult> AddComment([Bind("TvShowId,Name,Text")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.Date = DateTime.Now;
                _context.Comment.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = comment.TvShowId });
            }
            else
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }
            var tvShow = await _context.TvShow.Include(t => t.Comments).FirstOrDefaultAsync(m => m.Id == comment.TvShowId);
            return View("Details", tvShow);
        }
    }
}
