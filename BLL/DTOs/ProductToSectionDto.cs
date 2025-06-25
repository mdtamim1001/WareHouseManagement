using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class ProductToSectionDto
    {
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        public string SKU { get; set; }
        [Required]
        public int Quantity { get; set; }
        public DateTime ImportDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public int Addedby { get; set; }
        public int SectionId { get; set; }

    }
}
