using System;
using System.Collections.Generic;

namespace symphonylimited.Models;

public partial class Fee
{
    public int Id { get; set; }

    public int StdId { get; set; }

    public int CourseId { get; set; }

    public string Status { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;

    public virtual RegisteredStudent Std { get; set; } = null!;
}
