using BeachManage.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeachManage.Controllers
{
    public class PlayersDetailsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public PlayersDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            var model = _context.Players.Where(x => x.DateDeleteFromApp == null).ToList();
            return View(model);
        }
    }
}
