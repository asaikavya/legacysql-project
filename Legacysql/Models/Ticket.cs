using System;
using System.Collections.Generic;

namespace Legacysql.Models;

public partial class Ticket
{
    public int Id { get; set; }

    public string? IssueDescription { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedDate { get; set; }
}
