using System;
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
            return db.Users.Include("AssociatedManufaturer").FirstOrDefault(u => u.UserName == userName);
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
    }
}