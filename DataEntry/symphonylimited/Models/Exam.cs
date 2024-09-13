using System;
using System.Collections.Generic;

namespace symphonylimited.Models;

public partial class Exam
{
    public int ExamId { get; set; }

    public DateTime? Date { get; set; }

    public TimeSpan? Time { get; set; }

    public string? Venue { get; set; }

    public DateTime? ResultDate { get; set; }

    public virtual ICollection<EntranceStudent> EntranceStudents { get; set; } = new List<EntranceStudent>();
}
