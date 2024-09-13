using System;
using System.Collections.Generic;

namespace symphonylimited.Models;

public partial class About
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Discription { get; set; }

    public string? Image { get; set; }
}
