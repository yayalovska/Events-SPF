using System;
using System.Collections.Generic;

namespace EventsDomain.Models;

public partial class Event : Entity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public virtual ICollection<EventParticipation> EventParticipations { get; set; } = new List<EventParticipation>();
}
