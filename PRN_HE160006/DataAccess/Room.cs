using System;
using System.Collections.Generic;

namespace PRN_HE160006.DataAccess;

public partial class Room
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public string? Position { get; set; }

    public int? Status { get; set; }

    public string? Description { get; set; }
}
