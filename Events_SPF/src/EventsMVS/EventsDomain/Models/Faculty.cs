using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventsDomain.Models;

public partial class Faculty : Entity
{
    public new int Id { get; set; }

    [Display(Name = "Факультет")]
    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    public string Name { get; set; } = null!;

    public int DepartmentId { get; set; }

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual ICollection<EducationProgram> EducationPrograms { get; set; } = new List<EducationProgram>();
}
