using BeachManage.Models;
using BeachManage.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.EntityFrameworkCore;

namespace BeachManage.Controllers
{
    public class LiveMatchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LiveMatchController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DashBoard
        public async Task<ActionResult> IndexAsync(int teamA, int teamB)
        {



            var team1 = _context.Teams.Find(teamA);
            var team2 = _context.Teams.Find(teamB);





            var teamPlayers1 = _context.Teamplayers.Where(x => x.TeamId == teamA).FirstOrDefault();
            var teamPlayers2 = _context.Teamplayers.Where(x => x.TeamId == teamB).FirstOrDefault();

            var giocatoreUnoTeam1 = _context.Players.Where(x => x.Id == teamPlayers1.Player1Id).FirstOrDefault();
            var giocatoreDueTeam1 = _context.Players.Where(x => x.Id == teamPlayers1.Player2Id).FirstOrDefault();

            var giocatoreUnoTeam2 = _context.Players.Where(x => x.Id == teamPlayers2.Player1Id).FirstOrDefault();
            var giocatoreDueTeam2 = _context.Players.Where(x => x.Id == teamPlayers2.Player2Id).FirstOrDefault();


            var model = new MatchViewModel
            {
                Team1 = team1,
                Team2 = team2,
                GiocatoreTeam1Uno = giocatoreUnoTeam1.FirstName + " " + giocatoreUnoTeam1.LastName,
                GiocatoreTeam1Due = giocatoreDueTeam1.FirstName + " " + giocatoreDueTeam1.LastName,

                GiocatoreTeam2Uno = giocatoreUnoTeam2.FirstName + " " + giocatoreUnoTeam2.LastName,

                GiocatoreTeam2Due = giocatoreDueTeam2.FirstName + " " + giocatoreDueTeam2.LastName,
                
                PlayerIdUnoTeamUno = giocatoreUnoTeam1.Id,
                PlayerIdDueTeamUno = giocatoreDueTeam1.Id,
                PlayerIdUnoTeamDue = giocatoreUnoTeam2.Id,
                PlayerIdDueTeamDue = giocatoreDueTeam2.Id,

                Team1Score = 0,
                Team2Score = 0
            };



            var partita = new Match
            {
                TournamentId = 1,
                TeamAid = teamA,
                TeamBid = teamB,
                ScoreA = 0,
                ScoreB = 0,
                MatchDate = DateTime.Now,
                DateInsert = DateTime.Now,
            };


            _context.Matches.Add(partita);


            await _context.SaveChangesAsync();

            model.MatchId = _context.Matches
        .Where(m => m.TournamentId == 1 && m.TeamAid == teamA && m.TeamBid == teamB)
        .OrderByDescending(m => m.DateInsert)
        .Select(m => m.Id)
        .FirstOrDefault();


            return View(model);

        }

        [HttpPost]
        public IActionResult IncrementScore(int Team, int MatchId, int PlayerId)
        {
            var match = _context.Matches
                                .Include(m => m.TeamA)
                                .Include(m => m.TeamB)
                                .FirstOrDefault(m => m.Id == MatchId);

            if (match == null)
                return BadRequest("Partita non trovata");

     

            
       
            if (Team == 1 || Team == 2)
            {
                match.ScoreA++; // Incrementa il punteggio di squadra A
            }
            else
            {
                match.ScoreB++; // Incrementa il punteggio di squadra B
            }


            if(PlayerId != -1) {
                var statisticaPlayer = _context.Playerstats.FirstOrDefault(x => x.PlayerId == PlayerId && x.MatchId == MatchId && x.DateDeleteFromApp == null);


                if (statisticaPlayer == null)
                {
                    var statistica = new Playerstat
                    {
                        MatchId = MatchId,
                        PlayerId = PlayerId,
                        StatTypeId = 1,
                        StatValue = 1,
                        DateInsert = DateTime.Now,
                    };
                    _context.Playerstats.Add(statistica);
                }
                else
                {
                    statisticaPlayer.StatValue++;
                }
            }



            try
            {
                _context.SaveChanges();
                return Json(new { team1Score = match.ScoreA, team2Score = match.ScoreB });
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Zio cane errore");
            }
        }



        // GET: DashBoard/Details/5
        public ActionResult GetStats(int IdPlayer, int IdMatch)
        {
            var stats = _context.Stattypes.Where(x => x.Id != 1) //per non mostrare i punti
                               .Select(s => new { s.Id, s.Description })
                               .ToList();

            return Json(stats);
        }

        public ActionResult AddStats(int IdPlayer, int IdMatch, int Idstat)
        {

            var statistica = _context.Playerstats.Where(x => x.MatchId == IdMatch && x.PlayerId == IdPlayer && x.StatTypeId == Idstat && x.DateDeleteFromApp == null).FirstOrDefault();

            if(statistica != null)
            {
                statistica.StatValue++;
                _context.SaveChanges();

            }
            else
            {
                Playerstat playerstat = new Playerstat
                {
                    MatchId = IdMatch,
                    PlayerId = IdPlayer,
                    StatTypeId = Idstat,
                    StatValue = 1,
                    DateInsert = DateTime.Now,
                };
                _context.Playerstats.Add(playerstat);
                _context.SaveChanges();
            }

            return Json("OK");
        }

        // GET: DashBoard/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DashBoard/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EndMatch(int matchId)
        {
            try
            {
                var match = _context.Matches.Where(x => x.Id == matchId && x.DateDeleteFromApp == null).FirstOrDefault();
                if(match != null)
                    match.End = 1;
                _context.SaveChanges();
                return RedirectToAction("Index","Dashboard");
            }
            catch
            {
                return View();
            }
        }

        // GET: DashBoard/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DashBoard/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: DashBoard/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DashBoard/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}
