using BeachManage.Models;

namespace BeachManage.ViewModel
{
    public class MatchDetailViewModel
    {
        public string Team1 { get; set; }
        public string Team2 { get; set; }

        public string PlayerUnoTeam1 { get; set; }

        public string PlayerDueTeam1 { get; set; }

        public string PlayerUnoTeam2 { get; set; }

        public string PlayerDueTeam2 { get; set; }

        public List<Playerstat> PlayerUnoTeamUnostat { get; set; } = new List<Playerstat>();

        public List<Playerstat> PlayerDueTeamUnostat { get; set; } = new List<Playerstat>();

        public List<Playerstat> PlayerUnoTeamDuestat { get; set; } = new List<Playerstat>();

        public List<Playerstat> PlayerDueTeamDuestat { get; set; } = new List<Playerstat>();

        public List<Stattype> StatTypes { get; set; } = new List<Stattype>();
    }
}
