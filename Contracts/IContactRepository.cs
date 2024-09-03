using Entities.Models;

namespace Contracts
{
    public interface IContactRepository
    {
        Task<Contact> CreateContactAsync(Contact contact);
        Task<Contact> GetContactByIdAsync(Guid id, bool trackChanges);
        Task<IEnumerable<Contact>> GetAllContactAsync(bool trackChanges);
        void UpdateContact(Contact contact);
        void DeleteContact(Contact contact);
    }
}
