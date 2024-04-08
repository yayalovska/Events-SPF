using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventsDomain.Models;

public partial class Event : Entity
{
    public new int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Захід")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Час початку")]
    public DateTime? StartTime { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "Час кінця")]
    public DateTime? EndTime { get; set; }

    public virtual ICollection<EventParticipation> EventParticipations { get; set; } = new List<EventParticipation>();
}
