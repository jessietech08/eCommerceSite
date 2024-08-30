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

        public async Task<IActionResult> Index(int? id)
        {

            const int NumGamesToDisplayPerPage = 3;
            const int PageOffset = 1; // need a page offset to user current page and figure out num games to skip
            
            int currPage = id ?? 1; // set currPage to id if it has a value, otherwise use 1

            int totalNumOfProducts = await _context.Games.CountAsync();
            double maxNumPages = Math.Ceiling((double)totalNumOfProducts / NumGamesToDisplayPerPage);
            int lastPage = Convert.ToInt32(maxNumPages); // rounding pages up, to next whole page number
                                            

            // Get all games from the DB
            List<Game> games = await (from game in _context.Games
                                select game).Skip(NumGamesToDisplayPerPage * (currPage - PageOffset))
                                .Take(NumGamesToDisplayPerPage)
                                .ToListAsync();

            GameCatalogViewModel catalogViewModel = new(games, lastPage, currPage);
            return View(catalogViewModel);
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

        public async Task<IActionResult> Edit(int id)
        {
            Game? gameToEdit = await _context.Games.FindAsync(id);

            if (gameToEdit == null)
            {
                return NotFound();
            }
            return View(gameToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Game gameModel)
        {
            if (ModelState.IsValid)
            {
                _context.Games.Update(gameModel);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"{gameModel.Title} was updated successfully!";

                return RedirectToAction("Index");
            }

            return View(gameModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Game? gameToDelete = await _context.Games.FindAsync(id);

            if (gameToDelete == null)
            {
                return NotFound();
            }

            return View(gameToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Game gameToDelete = await _context.Games.FindAsync(id);

            if (gameToDelete != null) 
            {
                _context.Games.Remove(gameToDelete);
                await _context.SaveChangesAsync();
                TempData["Message"] = gameToDelete.Title + " was deleted successfully";
                return RedirectToAction("Index");

            }

            TempData["Message"] = "This was already deleted";
            return RedirectToAction("Index");
           
        }

        public async Task<IActionResult> Details(int id)
        {
            Game gameDetails = await _context.Games.FindAsync(id);

            if (gameDetails == null)
            {
                return NotFound();
            }
            return View(gameDetails);
        }

    }
}
