using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StartupPitchingPlatform.Data;
using StartupPitchingPlatform.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StartupPitchingPlatform.Controllers
{
   public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

public IActionResult Privacy()
    {
        return View();  // Make sure the view "Privacy" exists
    }
    public async Task<IActionResult> Index()
    {
        var posts = await _context.StartupPosts.ToListAsync();
        return View(posts);
    }

    public async Task<IActionResult> Search(string query)
    {
        if (string.IsNullOrEmpty(query))
        {
            // If the query is empty, return all posts
            var allPosts = await _context.StartupPosts.ToListAsync();
            return View("Index", allPosts);
        }

        // Filter posts by title or description containing the search query
        var searchResults = await _context.StartupPosts
            .Where(p => p.Category.ToLower().Contains(query.ToLower()) || p.Title.ToLower().Contains(query.ToLower()) || p.Description.ToLower().Contains(query.ToLower()))
            .ToListAsync();

        // Pass the search query to the view for display in the search box
        ViewBag.SearchQuery = query;

        return View("Index", searchResults);
    }

    public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var startupPost = await _context.StartupPosts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (startupPost == null)
            {
                return NotFound();
            }

            return View(startupPost);
        }
}


}
