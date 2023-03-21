using System;
using System.Collections.Generic;

namespace PRN_HE160006.DataAccess;

public partial class Contract
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    public int CustomerId { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }
    public int? Status { get; set; }
}
