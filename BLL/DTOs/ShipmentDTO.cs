using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class ShipmentDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public string Direction { get; set; } // "In" or "Out"

        [Required] 
        public int Quantity { get; set; }
        public DateTime ReleaseDate { get; set; }
        public DateTime ReachDate { get; set; }
        [Required]
        public string Status { get; set; }

    }
}
