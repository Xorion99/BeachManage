using System;
using System.Collections.Generic;

namespace BeachManage.Models;

public partial class Player
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime? DateInsert { get; set; }

    public DateTime? DateDeleteFromApp { get; set; }

    public virtual ICollection<Playerstat> Playerstats { get; set; } = new List<Playerstat>();

    public virtual ICollection<Teamplayer> TeamplayerPlayer1s { get; set; } = new List<Teamplayer>();

    public virtual ICollection<Teamplayer> TeamplayerPlayer2s { get; set; } = new List<Teamplayer>();
}
