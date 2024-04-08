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

    // Ця колекція представляє зв'язок один-до-багатьох між Faculty і Departments
    public virtual ICollection<Department> Departments { get; set; } = new HashSet<Department>();

    // Ця колекція представляє зв'язок один-до-багатьох між Faculty і EducationPrograms
    public virtual ICollection<EducationProgram> EducationPrograms { get; set; } = new List<EducationProgram>();

    // Конструктор без параметрів для ініціалізації колекцій
    public Faculty()
    {
        Departments = new HashSet<Department>();
        EducationPrograms = new List<EducationProgram>();
    }
}
