using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventsDomain.Models;

public partial class StudentParliamentMember : Entity
{
    [Key]
    public new int Id { get; set; }

    [Required]
    [ForeignKey("Студент")]
    public int StudentId { get; set; }

    [Required]
    [ForeignKey("Посада")]
    public int PositionId { get; set; }

    public virtual StudentParliamentPosition Position { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
