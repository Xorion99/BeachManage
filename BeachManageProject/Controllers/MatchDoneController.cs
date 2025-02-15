using BeachManage.Models;
using Microsoft.AspNetCore.Mvc;
using BeachManage.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace BeachManage.Controllers
{
    public class MatchDoneController : Controller
    {

        private readonly ApplicationDbContext _context;

        public MatchDoneController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var partite = _context.Matches
      .Where(m => m.End == 1 && m.DateDeleteFromApp == null) 
      .OrderByDescending(m => m.MatchDate) // Ordina per data (opzionale)
      .ToList();

            List<MatchDoneViewModel> model = new List<MatchDoneViewModel>();
            
            foreach(var Risultati in partite)
            {
                MatchDoneViewModel SingleModel = new MatchDoneViewModel();
                SingleModel.NomeSquadra1 = _context.Teams.Where(x => x.Id == Risultati.TeamAid).Select(x => x.Name).FirstOrDefault();
                SingleModel.NomeSquadra2 = _context.Teams.Where(x => x.Id == Risultati.TeamBid).Select(x => x.Name).FirstOrDefault();
                SingleModel.Score1 = Risultati.ScoreA;
                SingleModel.Score2 = Risultati.ScoreB;
                SingleModel.IdMatch = Risultati.Id;

                model.Add(SingleModel);


            }


            

            return View(model);
            
        }
    }
}
