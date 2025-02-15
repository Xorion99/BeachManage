using BeachManage.Models;

namespace BeachManage.ViewModel
{
    public class PlayerStatsViewModel
    {
        public List<Playerstat> PlayerStats { get; set; } = new List<Playerstat>();
        public List<Stattype> Stats { get; set; } = new List<Stattype>();
    }
}
