using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Repos
{
    public class SectionRepo : IRepo<Section, int, bool>
    {
        FirstDbContext db = new FirstDbContext();

        public bool Create(Section obj)
        {
            db.Sections.Add(obj);
            return db.SaveChanges() > 0;
        }

        public List<Section> Read()
        {
            return db.Sections.ToList();
        }

        public Section Read(int id)
        {
            return db.Sections.Find(id);
        }

        public bool Update(Section obj)
        {
            var ex = db.Sections.Find(obj.Id);
            if (ex == null) return false;

            db.Entry(ex).CurrentValues.SetValues(obj);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var data = db.Sections.Find(id);
            if (data == null) return false;

            db.Sections.Remove(data);
            return db.SaveChanges() > 0;
        }
    }
}
