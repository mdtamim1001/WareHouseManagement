using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class SectionProducts
    {
        public int Id { get; set; }
        [ForeignKey("Section")]
        public int SectionID { get; set; }
        public virtual Section Section { get; set; }
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
