using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.interfaces;
using DAL.Models;

namespace DAL.repos
{
    public class SectionProductRepo : IRepo<SectionProducts, int, bool>
    {
        FirstDbContext db = new FirstDbContext();

        public bool Create(SectionProducts obj)
        {
            db.SectionProducts.Add(obj);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var data = db.SectionProducts.Find(id);
            if (data == null) return false;

            db.SectionProducts.Remove(data);
            return db.SaveChanges() > 0;
        }

        public List<SectionProducts> Read()
        {
            return db.SectionProducts
                     .Include("Section")
                     .Include("Product")
                     .ToList();
        }

        public SectionProducts Read(int id)
        {
            return db.SectionProducts
                     .Include("Section")
                     .Include("Product")
                     .FirstOrDefault(sp => sp.Id == id);
        }

        public bool Update(SectionProducts obj)
        {
            var existing = db.SectionProducts.Find(obj.Id);
            if (existing == null) return false;

            // Update properties
            existing.SectionID = obj.SectionID;
            existing.ProductID = obj.ProductID;
            existing.Quantity = obj.Quantity;

            return db.SaveChanges() > 0;
        }
    }


}
