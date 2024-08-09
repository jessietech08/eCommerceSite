using eCommerceSite.Data;
using eCommerceSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceSite.Controllers
{
    public class GamesController : Controller
    {
        private readonly VideoGameContext _context;

        public GamesController(VideoGameContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Get all games from the DB
            //List<Game> games = _context.Games.ToList();
            List<Game> games = await (from game in _context.Games
                                select game).ToListAsync();
            // Show them on the page 

            return View(games);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Game g)
        {
            if (ModelState.IsValid)
            {
                _context.Games.Add(g); // Prepares insert
                await _context.SaveChangesAsync(); // Execute pending insert

                // Show success message on page
                ViewData["Message"] = $"{g.Title} was added successfully";
                return View();
            }
            return View(g);
        }


    }
}
