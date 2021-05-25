﻿using System;
using System.Collections.Generic;

#nullable disable

namespace BetaCinemas.Models
{
    public partial class Seat
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public bool IsEmpty { get; set; }

        public virtual Room IdNavigation { get; set; }
    }
}