using ContactManagerApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManger.Services
{
    public class ContactService: IContactService
    {
        private readonly ContactContext _context;

        public ContactService(ContactContext context)
        {
            _context = context;
        }
  
        public async Task<List<Contacts>> GetContacts()
        {
           return await _context.Contacts.ToListAsync();
        }

        public async Task<Contacts> GetContactById(int id)
        {
           return await  _context.Contacts
                .SingleOrDefaultAsync(m => m.ContactId == id);

        }

        public async Task<int> AddContact(ContactRequest contact)
        {

            string validations = VaidateContact(contact);
            if (!string.IsNullOrEmpty(validations))
                throw new Exception(validations);

            Contacts addContact = new Contacts();
            addContact.FirstName = contact.FirstName;
            addContact.LastName = contact.LastName;
            addContact.Email = contact.Email;
            addContact.PhoneNumber = contact.PhoneNumber;
            addContact.Status = "Active";
            await _context.Contacts.AddAsync(addContact);
            await _context.SaveChangesAsync();

            return addContact.ContactId;

        }

        public async Task<bool> UpdateContact(int id, ContactRequest contact)
        {

            string validations = VaidateContact(contact);
            if(!string.IsNullOrEmpty(validations))
                throw new Exception(validations);

            var findContact =  _context.Contacts.Where(c=>c.ContactId==id && c.Status=="Active").FirstOrDefault();
            if (findContact == null)
            {
                throw new Exception("Contact not found");
            }
            findContact.FirstName = contact.FirstName;
            findContact.LastName = contact.LastName;
            findContact.Email = contact.Email;
            findContact.PhoneNumber = contact.PhoneNumber;
            _context.Contacts.Update(findContact);
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<bool> DeleteContact(int id)
        {
            var findContact = await _context.Contacts.FindAsync(id);
            if (findContact == null)
            {
                throw new Exception("Contact not found");
            }
            findContact.Status = "InActive";
            _context.Contacts.Update(findContact);
            await _context.SaveChangesAsync();

            return true;
        }

        private string VaidateContact(ContactRequest contact)
        {
            StringBuilder sbValidations = new StringBuilder();
            if (string.IsNullOrEmpty(contact.FirstName) || string.IsNullOrWhiteSpace(contact.FirstName))
                sbValidations.AppendLine("Required First Name");

            if (string.IsNullOrEmpty(contact.LastName) || string.IsNullOrWhiteSpace(contact.LastName))
                sbValidations.AppendLine("Required Last Name");

            if (string.IsNullOrEmpty(contact.Email) || string.IsNullOrWhiteSpace(contact.Email))
                sbValidations.AppendLine("Required Email");

            if (string.IsNullOrEmpty(contact.PhoneNumber) || string.IsNullOrWhiteSpace(contact.PhoneNumber))
                sbValidations.AppendLine("Required PhoneNumber");

            return sbValidations.ToString();

        }




    }
}
