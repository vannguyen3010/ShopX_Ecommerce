using Entities.Models;

namespace Contracts
{
    public interface IContactRepository
    {
        Task<Contact> CreateContactAsync(Contact contact);
        Task<Contact> GetContactByIdAsync(Guid id, bool trackChanges);
        Task<(IEnumerable<Contact> Contacts, int Total)> GetAllContactAsync(bool trackChanges, int type, int pageNumber, int pageSize);
        void UpdateContact(Contact contact);
        void DeleteContact(Contact contact);
    }
}
