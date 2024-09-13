using System;
using System.Collections.Generic;

namespace symphonylimited.Models;

public partial class RegisteredStudent
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNo { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string FeeStatus { get; set; } = null!;

    public int CourseId { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<Fee> Fees { get; set; } = new List<Fee>();
}
