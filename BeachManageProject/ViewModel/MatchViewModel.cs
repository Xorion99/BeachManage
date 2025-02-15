using BeachManage.Models;

namespace BeachManage.ViewModel
{
    public class MatchViewModel
    {
        
        public int MatchId { get; set; }
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }

        
        public string GiocatoreTeam1Uno { get; set; }
        public string GiocatoreTeam1Due { get; set; }
        public string GiocatoreTeam2Uno { get; set; }

        public string GiocatoreTeam2Due { get; set; }

        public int PlayerIdUnoTeamUno { get; set; }
        public int PlayerIdDueTeamUno { get; set; }

        public int PlayerIdUnoTeamDue { get; set; }
        public int PlayerIdDueTeamDue { get; set; }


        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
    }

}
