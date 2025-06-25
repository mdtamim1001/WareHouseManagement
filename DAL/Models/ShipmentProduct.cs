using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ShipmentProduct
    {
        public int Id { get; set; }

        [ForeignKey("Shipment")]
        public int ShipmentId { get; set; }
        public virtual Shipment Shipment { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int Quantity { get; set; } 
    }
}
