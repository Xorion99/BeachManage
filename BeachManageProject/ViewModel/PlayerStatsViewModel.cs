using BeachManage.Models;

namespace BeachManage.ViewModel
{
    public class PlayerStatsViewModel
    {
        public List<Player> Players { get; set; } = new();
        public List<Stattype> Stats { get; set; } = new();
        public List<Playerstat> PlayerStats { get; set; } = new();
        public int? SelectedPlayerId { get; set; }
    }
}
