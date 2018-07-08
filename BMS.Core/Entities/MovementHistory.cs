using BMS.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BMS.Core.Entities
{
    class MovementHistory:BaseEntity
    {
    public Inventory Inventory { get; set; }
    public int Quantity { get; set; }
    public MovementDirection Direction { get; set; }
    public BinLocation FromLocation { get; set; }
    public BinLocation ToLocation { get; set; }
  }
}
