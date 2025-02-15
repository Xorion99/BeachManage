using BeachManage.Models;
using BeachManage.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BeachManage.Controllers
{
    public class MatchDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MatchDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int id)
        {
            
            MatchDetailViewModel model = new MatchDetailViewModel();

            model.StatTypes = _context.Stattypes.ToList();


            List<Playerstat> GameStats = _context.Playerstats.Where(x => x.MatchId == id && x.DateDeleteFromApp == null).ToList();



            var match = _context.Matches.Where(x => x.Id == id && x.DateDeleteFromApp == null).FirstOrDefault();
            Teamplayer? playersA = _context.Teamplayers.Where(x => x.TeamId == match.TeamAid && x.DateDeleteFromApp == null).FirstOrDefault();
            Teamplayer? playersB = _context.Teamplayers.Where(x => x.TeamId == match.TeamBid && x.DateDeleteFromApp == null).FirstOrDefault();
            List<string> NamePlayer = GetNamePlayer(playersA, playersB);

            model.Team1 = _context.Teams.Where(x => x.Id == match.TeamAid).Select(x => x.Name).FirstOrDefault();
            model.Team2 = _context.Teams.Where(x => x.Id == match.TeamBid).Select(x => x.Name).FirstOrDefault();






            //TODO 
            //PER IL MMOMENTO LA LISTA E' GESTITA IN QUESTO MODO
            //sono 4 elelementi ["giocator1Team1,Giocator2Team1,Gicatore1Team2,giocatore2Team2]
            //tutto sto schifo l ofa la funzione GetNamePlayer() fatto in fretta da REFACTORARE 


            model.PlayerUnoTeam1 = NamePlayer[0];
            model.PlayerDueTeam1 = NamePlayer[1];
            model.PlayerUnoTeam2 = NamePlayer[2];
            model.PlayerDueTeam2 = NamePlayer[3];

            foreach(var stats in GameStats)
            {
                if(stats.PlayerId == playersA.Player1Id)
                {
                    model.PlayerUnoTeamUnostat.Add(stats);
                }
                if (stats.PlayerId == playersA.Player2Id)
                {
                    model.PlayerDueTeamUnostat.Add(stats);
                }
                if (stats.PlayerId == playersB.Player1Id)
                {
                    model.PlayerUnoTeamDuestat.Add(stats);
                }
                if (stats.PlayerId == playersB.Player2Id)
                {
                    model.PlayerDueTeamDuestat.Add(stats);
                }

            }






            return View(model);
        }




        public List<string> GetNamePlayer(Teamplayer pl1, Teamplayer pl2)
        {




            List<string> ListaNomi = new List<string>();

            string nome_Player1Team1 = _context.Players.Where(x => x.Id == pl1.Player1Id && x.DateDeleteFromApp == null).Select(x => x.FirstName ).FirstOrDefault();
            string cognome_Player1Team1 = _context.Players.Where(x => x.Id == pl1.Player1Id && x.DateDeleteFromApp == null).Select(x => x.LastName).FirstOrDefault();

            ListaNomi.Add(nome_Player1Team1 + " " + cognome_Player1Team1);

            string nome_Player2Team1 = _context.Players.Where(x => x.Id == pl1.Player2Id && x.DateDeleteFromApp == null).Select(x => x.FirstName).FirstOrDefault();
            string cognome_Player2Team1 = _context.Players.Where(x => x.Id == pl1.Player2Id && x.DateDeleteFromApp == null).Select(x => x.LastName).FirstOrDefault();

            ListaNomi.Add(nome_Player2Team1 + " " + cognome_Player2Team1);

            string nome_Player1Team2 = _context.Players.Where(x => x.Id == pl2.Player1Id && x.DateDeleteFromApp == null).Select(x => x.FirstName).FirstOrDefault();
            string cognome_Player1Team2 = _context.Players.Where(x => x.Id == pl2.Player1Id && x.DateDeleteFromApp == null).Select(x => x.LastName).FirstOrDefault();

            ListaNomi.Add(nome_Player1Team2 + " " + cognome_Player1Team2);

            string nome_Player2Team2 = _context.Players.Where(x => x.Id == pl2.Player2Id && x.DateDeleteFromApp == null).Select(x => x.FirstName).FirstOrDefault();
            string cognome_Player2Team2 = _context.Players.Where(x => x.Id == pl2.Player2Id && x.DateDeleteFromApp == null).Select(x => x.LastName).FirstOrDefault();

            ListaNomi.Add(nome_Player2Team2 + " " + cognome_Player2Team2);

            return ListaNomi;

        }





    }



}
