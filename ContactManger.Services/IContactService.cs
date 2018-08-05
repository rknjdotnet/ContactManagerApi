using ContactManagerApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ContactManger.Services
{
    public interface IContactService
    {
        Task<List<Contacts>> GetContacts();
        Task<Contacts> GetContactById(int id);
        Task<int> AddContact(ContactRequest contact);
        Task<bool> UpdateContact(int id, ContactRequest contact);
        Task<bool> DeleteContact(int id);
    }
}
