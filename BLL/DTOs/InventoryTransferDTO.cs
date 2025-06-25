using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class InventoryTransferDTO
    {
        public int InventoryId { get; set; }
        public int FromSectionId { get; set; }
        public int ToSectionId { get; set; }
        public int Quantity { get; set; }
    }
}
