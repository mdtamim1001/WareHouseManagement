using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Repos
{
    public class ProductRepo : IRepo<Product, int, bool> 
    {
        FirstDbContext db = new FirstDbContext();

        public bool Create(Product obj)
        {
            db.Products.Add(obj);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> Read()
        {
            return db.Products.ToList();
        }

        public Product Read(int id)
        {
            return db.Products.Find(id);
        }

        public bool Update(Product obj)
        {
            var ex = db.Products.Find(obj.Id);
            if (ex == null) return false;

            db.Entry(ex).CurrentValues.SetValues(obj);
            return db.SaveChanges() > 0;
        }

        
    }
}
