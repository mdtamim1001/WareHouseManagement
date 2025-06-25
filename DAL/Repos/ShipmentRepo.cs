using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Repos
{
    public class ShipmentRepo : IRepo<Shipment, int, bool>
    {
        FirstDbContext db = new FirstDbContext();

        public bool Create(Shipment obj)
        {
            db.Shipments.Add(obj);
            return db.SaveChanges() > 0;
        }

        public List<Shipment> Read()
        {
            return db.Shipments.ToList();
        }

        public Shipment Read(int id)
        {
            return db.Shipments.Find(id);
        }

        public bool Update(Shipment obj)
        {
            var ex = db.Shipments.Find(obj.Id);
            if (ex == null) return false;

            db.Entry(ex).CurrentValues.SetValues(obj);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var data = db.Shipments.Find(id);
            if (data == null) return false;

            db.Shipments.Remove(data);
            return db.SaveChanges() > 0;
        }
    }
}
