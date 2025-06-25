using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Section
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int MaxQuantity { get; set; }
        public int Quantity { get; set; }

        public virtual ICollection<SectionProducts> SectionProducts { get; set; }

        public Section()
        {
            SectionProducts = new List<SectionProducts>();
        }
    }


}
