using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string SKU { get; set; }

        public int Quantity { get; set; }

        public DateTime ImportDate { get; set; }
        public DateTime ExpireDate { get; set; }

        [ForeignKey("Manager")]
        public int Addedby { get; set; }
        public virtual Manager Manager { get; set; }

        public virtual ICollection<SectionProducts> SectionProducts { get; set; }
        public virtual ICollection<ShipmentProduct> ShipmentProducts { get; set; }

        public Product()
        {
            SectionProducts = new List<SectionProducts>();
            ShipmentProducts = new List<ShipmentProduct>();
        }
    }

}
