using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventsDomain.Models;

public partial class EducationProgram : Entity
{
    public new int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Освітня програма")]
    public string? Name { get; set; }

    [Display(Name = "Факультет")]
    public int FacultyId { get; set; }

    public virtual Faculty Faculty { get; set; } = null!;
}
