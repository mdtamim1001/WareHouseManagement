using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class ManagerRepo : IRepo<Manager, string, Manager>, IAuth<bool>
    {
        FirstDbContext db = new FirstDbContext();
        public bool Authenticate(int id, string password)
        {
            var data = db.Managers.FirstOrDefault(u=>u.Id.Equals(id) &&
            u.PasswordHash.Equals(password));
            if(data !=null) return true;
            return false;
        }

        public Manager Create(Manager obj)
        {
            db.Managers.Add(obj);
            if (db.SaveChanges() > 0) return obj;
            return null;
        }

        public bool Delete(string id)
        {
            var ex = Read(id);
            db.Managers.Remove(ex);
            return db.SaveChanges() > 0;
        }

        public List<Manager> Read()
        {
            return db.Managers.ToList();
        }

        public Manager Read(string id)
        {
            return db.Managers.Find(id);
        }

        public Manager Update(Manager obj)
        {
            var ex = Read(obj.FullName);
            db.Entry(ex).CurrentValues.SetValues(obj);
            if (db.SaveChanges() > 0) return obj;
            return null;

        }
    }
}
