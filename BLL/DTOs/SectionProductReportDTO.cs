using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class SectionProductReportDTO
    {
        public string SectionName { get; set; }
        public List<ProductInSectionDTO> Products { get; set; }
    }

    public class ProductInSectionDTO
    {
        public string ProductName { get; set; }

        public string SKU { get; set; }
        public int Quantity { get; set; }
    }

}
