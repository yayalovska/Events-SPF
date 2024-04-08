using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventsDomain.Models;

public partial class Student : Entity
{
    public new int Id { get; set; }

    [Required(ErrorMessage = "Поле не повинно бути порожнім")]
    [Display(Name = "ПІБ")]
    public string FullName { get; set; } = null!;

    [DataType(DataType.Date)]
    [Display(Name = "Дата народження")]
    public DateOnly? BirthDate { get; set; }

    [Display(Name = "Департамент")]
    public int DepartmentId { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<EventParticipation> EventParticipations { get; set; } = new List<EventParticipation>();

    public virtual ICollection<StudentParliamentMember> StudentParliamentMembers { get; set; } = new List<StudentParliamentMember>();

    public Student()
    {
        EventParticipations = new HashSet<EventParticipation>();
        StudentParliamentMembers = new HashSet<StudentParliamentMember>();
    }
}
