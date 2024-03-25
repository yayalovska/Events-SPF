using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventsDomain.Models;

public partial class Department : Entity
{
    public new int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Департамент")]
    public string? Name { get; set; }
    
    [Display(Name = "Факультет")]
    public int FacultyId { get; set; }
    
    

    public virtual Faculty Faculty { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
