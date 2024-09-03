using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class ContactRepository : RepositoryBase<Contact>, IContactRepository
    {
        private readonly RepositoryContext _dbContext;

        public ContactRepository(RepositoryContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Contact> CreateContactAsync(Contact contact)
        {
            await _dbContext.Contacts.AddAsync(contact);
            await _dbContext.SaveChangesAsync();

            return contact;
        }

        public async Task<Contact> GetContactByIdAsync(Guid id, bool trackChanges)
        {
            return await FindByCondition(x => x.Id.Equals(id), trackChanges).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Contact>> GetAllContactAsync(bool trackChanges)
        {
            return await FindAll(trackChanges).OrderBy(x => x.Name).ToListAsync();
        }

        public void UpdateContact(Contact contact)
        {
            Update(contact);
        }

        public void DeleteContact(Contact contact)
        {
            Delete(contact);
        }
    }
}
