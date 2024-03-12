using System;
using System.Collections.Generic;

namespace EventsDomain.Models;

public partial class Department : Entity
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int FacultyId { get; set; }

    public virtual Faculty Faculty { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
