using System;
using System.Collections.Generic;

namespace EventsDomain.Models;

public partial class Faculty : Entity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual ICollection<EducationProgram> EducationPrograms { get; set; } = new List<EducationProgram>();
}
