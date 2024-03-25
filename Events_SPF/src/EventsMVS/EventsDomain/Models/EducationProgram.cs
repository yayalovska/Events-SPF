using System;
using System.Collections.Generic;

namespace EventsDomain.Models;

public partial class EducationProgram : Entity
{
    public new int Id { get; set; }

    public string? Name { get; set; }

    public int FacultyId { get; set; }

    public virtual Faculty Faculty { get; set; } = null!;
}
