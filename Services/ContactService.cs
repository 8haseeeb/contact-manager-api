using ContactManagerApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace ContactManagerApi.Services
{
    public class ContactService
    {
        private readonly List<Contact> _contacts = new();
        private int _nextId = 1;

        public IEnumerable<Contact> GetAll() => _contacts;
        public Contact? GetById(int id) => _contacts.FirstOrDefault(c => c.Id == id);
        public Contact Add(Contact contact)
        {
            contact.Id = _nextId++;
            _contacts.Add(contact);
            return contact;
        }
        public bool Update(int id, Contact contact)
        {
            var existing = GetById(id);
            if (existing == null) return false;
            existing.FullName = contact.FullName;
            existing.Email = contact.Email;
            existing.PhoneNumber = contact.PhoneNumber;
            return true;
        }
        public bool Delete(int id)
        {
            var contact = GetById(id);
            if (contact == null) return false;
            _contacts.Remove(contact);
            return true;
        }
    }
} 