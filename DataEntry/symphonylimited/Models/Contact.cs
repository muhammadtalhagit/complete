using System;
using System.Collections.Generic;

namespace symphonylimited.Models;

public partial class Contact
{
    public int Id { get; set; }

    public string BranchDetails { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Image { get; set; } = null!;
}
