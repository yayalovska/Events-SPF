using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventsDomain.Models;

public partial class Faculty : Entity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public new int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Факультет")]
    public string Name { get; set; } = null!;

    public virtual ICollection<Department> Departments { get; set; } = new HashSet<Department>();

    public virtual ICollection<EducationProgram> EducationPrograms { get; set; } = new List<EducationProgram>();

    public Faculty()
    {
        Departments = new HashSet<Department>();
        EducationPrograms = new List<EducationProgram>();
    }
}
