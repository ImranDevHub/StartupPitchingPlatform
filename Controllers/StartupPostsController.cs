using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StartupPitchingPlatform.Data;
using StartupPitchingPlatform.Models;

namespace StartupPitchingPlatform.Controllers
{
    [Authorize]
    public class StartupPostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StartupPostsController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: StartupPosts
        public async Task<IActionResult> Index()
{
    // Get the current user's ID
    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    if (userId == null)
    {
        return Unauthorized(); // Handle the case where the user is not authenticated
    }

    // Filter posts by the current user's AuthorId
    var userPosts = await _context.StartupPosts
        .Where(p => p.AuthorId == userId)
        .ToListAsync();

    return View(userPosts);
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

        // GET: StartupPosts/Details/5
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

        // GET: StartupPosts/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: StartupPosts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ImageUrl,Category,GitHubUrl,LinkedInUrl")] StartupPost startupPost)
        {
            if (ModelState.IsValid)
            {
                // Set the AuthorId to the current user's ID
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId == null)
                {
                    return Unauthorized(); // Handle the case where the user is not authenticated
                }

                startupPost.AuthorId = userId; // Ensure this line is present
                startupPost.PostedOn = DateTime.Now;

                // Log the AuthorId for debugging
                Console.WriteLine($"AuthorId: {startupPost.AuthorId}");

                _context.Add(startupPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Log or debug ModelState errors
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage); // Or use a logging framework
            }

            return View(startupPost);
        }

        // GET: StartupPosts/Edit/5
        [Authorize]
public async Task<IActionResult> Edit(int? id)
{
    if (id == null)
    {
        return NotFound();
    }

    var startupPost = await _context.StartupPosts.FindAsync(id);
    if (startupPost == null)
    {
        return NotFound();
    }

    // Check if the current user is the author
    if (startupPost.AuthorId != User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
    {
        return Forbid(); // Or return a custom error view
    }

    return View(startupPost);
}
        // POST: StartupPosts/Edit/5
        [HttpPost]
[ValidateAntiForgeryToken]
[Authorize]
public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ImageUrl,Category,GitHubUrl,LinkedInUrl")] StartupPost startupPost)
{
    if (id != startupPost.Id)
    {
        return NotFound();
    }

    // Check if the current user is the author
    var existingPost = await _context.StartupPosts.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    if (existingPost == null || existingPost.AuthorId != User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
    {
        return Forbid(); // Or return a custom error view
    }

    if (ModelState.IsValid)
    {
        try
        {
            // Preserve the AuthorId and PostedOn values
            startupPost.AuthorId = existingPost.AuthorId;
            startupPost.PostedOn = existingPost.PostedOn;

            _context.Update(startupPost);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StartupPostExists(startupPost.Id))
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
    return View(startupPost);
}
        // GET: StartupPosts/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
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

            // Check if the current user is the author
            if (startupPost.AuthorId != User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            {
                return Forbid(); // Or return a custom error view
            }

            return View(startupPost);
        }

        // POST: StartupPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var startupPost = await _context.StartupPosts.FindAsync(id);
            if (startupPost == null)
            {
                return NotFound();
            }

            // Check if the current user is the author
            if (startupPost.AuthorId != User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            {
                return Forbid(); // Or return a custom error view
            }

            _context.StartupPosts.Remove(startupPost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StartupPostExists(int id)
        {
            return _context.StartupPosts.Any(e => e.Id == id);
        }
    }
}
