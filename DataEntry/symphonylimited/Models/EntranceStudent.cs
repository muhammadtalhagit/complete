using System;
using System.Collections.Generic;

namespace symphonylimited.Models;

public partial class EntranceStudent
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int ExamId { get; set; }

    public string PhoneNo { get; set; } = null!;

    public string Result { get; set; } = null!;

    public string? Address { get; set; }

    public string FeeStatus { get; set; } = null!;

    public int? CourseId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Exam Exam { get; set; } = null!;
}
