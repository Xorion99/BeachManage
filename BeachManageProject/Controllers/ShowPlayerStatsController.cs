using BeachManage.Models;
using BeachManage.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BeachManage.Controllers
{
    public class ShowPlayerStatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShowPlayerStatsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? PlayerId)
        {
            PlayerStatsViewModel model = new PlayerStatsViewModel();
            model.Stats = _context.Stattypes.Where(x => x.DateDeleteFromApp == null).ToList();
            model.Players = _context.Players.Where(x => x.DateDeleteFromApp == null).ToList();
            model.SelectedPlayerId = PlayerId;

            if (PlayerId.HasValue && PlayerId.Value > 0)
            {
                // Carichiamo solo le statistiche del giocatore selezionato
                model.PlayerStats = _context.Playerstats
                    .Where(x => x.DateDeleteFromApp == null && x.PlayerId == PlayerId.Value)
                    .ToList();
            }
            else
            {
                // Carichiamo tutte le statistiche di tutti i giocatori
                model.PlayerStats = _context.Playerstats
                    .Where(x => x.DateDeleteFromApp == null)
                    .ToList();
            }

            return View(model);
        }


    }
}
