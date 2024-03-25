using System;
using System.Collections.Generic;

namespace EventsDomain.Models;

public partial class StudentParliamentMember : Entity
{
    public new int Id { get; set; }

    public int StudentId { get; set; }

    public int PositionId { get; set; }

    public virtual StudentParliamentPosition Position { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
