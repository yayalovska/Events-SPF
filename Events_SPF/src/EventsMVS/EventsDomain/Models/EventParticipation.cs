using System;
using System.Collections.Generic;

namespace EventsDomain.Models;

public partial class EventParticipation : Entity
{
    public int StudentId { get; set; }

    public int EventId { get; set; }

    public string? Result { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
