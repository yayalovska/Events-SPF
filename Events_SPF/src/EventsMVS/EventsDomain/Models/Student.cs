using System;
using System.Collections.Generic;

namespace EventsDomain.Models;

public partial class Student : Entity
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public DateOnly? BirthDate { get; set; }

    public int DepartmentId { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<EventParticipation> EventParticipations { get; set; } = new List<EventParticipation>();

    public virtual ICollection<StudentParliamentMember> StudentParliamentMembers { get; set; } = new List<StudentParliamentMember>();
}
