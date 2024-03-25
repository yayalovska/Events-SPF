using System;
using System.Collections.Generic;

namespace EventsDomain.Models;

public partial class StudentParliamentPosition : Entity
{
    public new int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<StudentParliamentMember> StudentParliamentMembers { get; set; } = new List<StudentParliamentMember>();
}
