using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Shipment
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        public string Direction { get; set; } // "In" or "Out"

        public int Quantity { get; set; }

        public DateTime ReleaseDate { get; set; }
        public DateTime ReachDate { get; set; }

        public string Status { get; set; }

        [ForeignKey("Manager")]
        public int Addedby { get; set; }
        public virtual Manager Manager { get; set; }

        public virtual ICollection<ShipmentProduct> ShipmentProducts { get; set; }

        public Shipment()
        {
            ShipmentProducts = new List<ShipmentProduct>();
        }
    }


}
