using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnicefVirtualWarehouse.Models.Repositories
{
    public class ContactRepository
    {
        private UnicefContext db;
        
        public ContactRepository()
        {
            db = MvcApplication.CurrentUnicefContext;
        }

        public void Update(int id, Contact newContact)
        {
            var oldContact = GetById(id);
            oldContact.Address = newContact.Address;
            oldContact.City = newContact.City;
            oldContact.Email = newContact.Email;
            oldContact.Fax = newContact.Fax;
            oldContact.Phone = newContact.Phone;
            oldContact.Website = newContact.Website;
            oldContact.Zip = newContact.Zip;

            db.SaveChanges();
        }

        public Contact GetById(int id)
        {
            return db.Contacts.Where(c => c.Id == id).FirstOrDefault();
        }

        public void Create(Contact contact)
        {
            db.Contacts.Add(contact);
            db.SaveChanges();
        }
    }
}