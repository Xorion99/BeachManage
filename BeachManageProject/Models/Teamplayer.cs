using System;
using System.Collections.Generic;

namespace BeachManage.Models;

public partial class Teamplayer
{
    public int Id { get; set; }

    public int TeamId { get; set; }

    public int Player1Id { get; set; }

    public int Player2Id { get; set; }

    public DateTime? DateInsert { get; set; }

    public DateTime? DateDeleteFromApp { get; set; }

    public virtual Player Player1 { get; set; } = null!;

    public virtual Player Player2 { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;
}
