using Microsoft.AspNetCore.Mvc;
using BeachManage.Models;
using System.Linq;

public class NewMatch : Controller
{
    private readonly ApplicationDbContext _context;

    public NewMatch(ApplicationDbContext context)
    {
        _context = context;
    }

    public ActionResult Index()
    {
        
        var teams = _context.Teams
            .Where(t => t.DateDeleteFromApp == null) 
            .ToList();

        return View(teams);
    }


    [HttpPost]
    public IActionResult StartMatch(int teamA, int teamB)
    {
   
        var match = new Match
        {
            TeamAid = teamA,
            TeamBid = teamB,
            DateInsert = DateTime.Now
        };

        _context.Matches.Add(match);
        _context.SaveChanges();

        TempData["Success"] = "Partita creata con successo!";
        return RedirectToAction("Index", "LiveMatch");
    }
}

