using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace UnicefVirtualWarehouse.Models.Repositories
{
    public class UserRepository
    {
        private UnicefContext db;

        public UserRepository()
        {
            db = MvcApplication.CurrentUnicefContext;
        }

        public bool Add(User newUser)
        {
            if (db.Users.FirstOrDefault(u => u.UserName == newUser.UserName) != null)
                return false;

            db.Users.Add(newUser);
            return db.SaveChanges() > 0;
        }

        public User GetByName(string userName)
        {
            return WithAllFields(db.Users).FirstOrDefault(u => u.UserName == userName);
        }

        public bool Delete(User user)
        {
            db.Users.Remove(user);
            return db.SaveChanges() > 0;
        }

        public bool Delete(string userName)
        {
            var user = GetByName(userName);
            return Delete(user);
        }

        public User GetById(int userId)
        {
            return db.Users.Include("AssociatedManufaturer").FirstOrDefault(u => u.Id == userId);
        }

        private DbQuery<User> WithAllFields(DbSet<User> users)
        {
            return users.Include("AssociatedManufaturer").Include("AssociatedManufaturer.Contact");
        }

        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }
    }
}