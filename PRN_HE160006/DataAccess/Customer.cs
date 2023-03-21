using System;
using System.Collections.Generic;

namespace PRN_HE160006.DataAccess;

public partial class Customer
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }
}
