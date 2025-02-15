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
        public IActionResult Index(int PlayerId)
        {
            PlayerStatsViewModel model = new PlayerStatsViewModel();
            model.Stats = _context.Stattypes.Where(x => x.DateDeleteFromApp == null).ToList();

            var StatsPlayer = _context.Playerstats.Where(x => x.DateDeleteFromApp == null && x.PlayerId == PlayerId).ToList();

            model.PlayerStats = StatsPlayer;

            return View(model);
        }
    }
}
