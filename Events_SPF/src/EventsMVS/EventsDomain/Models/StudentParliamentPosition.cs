using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventsDomain.Models;

public partial class StudentParliamentPosition : Entity
{
    [Key]
    public new int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Посада")]
    public string? Name { get; set; }

    public virtual ICollection<StudentParliamentMember> StudentParliamentMembers { get; set; } = new List<StudentParliamentMember>();
}
