using Contracts;
using Entities.Models;
using Entities.Models.Address;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    // tạo class controllerAPI -> microsoft khuyến khích gắn đuôi , tác dụng để phân biệt 
    // ví dụ UserControllers
    // dự án của em là nhỏ , ko có khả năng tái chế , ko có lớp bảo mật , khả năng nâng cấp rất khó 
    // nếu triển khai module thì phải viết lại hoàn toàn .
    // anh ví dụ cho em viết web bán hàng , web truyện tranh , web tin tức thì em làm thế nào 
    // xác định cái phần nào chung , riêng , mặc định 3 dạng web ở trên điều phải authentication , viết lại tốn time , 
    // ví dụ cao hơn xíu , nếu để tạo 1 web tốn 2 tháng mà bảo mật ko có vậy 1 năm tạo dc có 6 cái web , em lên trình kiểu gì 
    // trong khi người khác code cũng bằng c# , code 2 ngày tương đương em code 3 tháng
    // trường hợp muốn seo tốt , dùng angular universal
    //net 8 net 9 tiêm phụ thuộc trực tiếp trên class -> languages 12 c#
    //public class AddressServicesDB(RepositoryContext dbContext)
    //{
    //    public async Task CreateAddressAsync(Address address)
    //    {
    //        await dbContext.Addresses.AddAsync(address);
    //        await dbContext.SaveChangesAsync();
    //    }
    //}


    public class AddressRepository : RepositoryBase<Address>, IAddressRepository
    {
        private readonly RepositoryContext _dbContext;

        public AddressRepository(RepositoryContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateAddressAsync(Address address)
        {
            await _dbContext.Addresses.AddAsync(address);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Address> GetAddressByIdAsync(Guid addressId, bool trackChanges)
        {
            return await FindByCondition(address => address.Id.Equals(addressId), trackChanges)
                .FirstOrDefaultAsync();
        }

        public async Task<Address> GetAddressByNameAsync(string name)
        {
            var lowerName = name.ToLower(); // Chuyển đổi tên danh mục về dạng chữ thường
            return await _dbContext.Addresses
                .Where(x => x.StreetAddress.ToLower() == lowerName)// So sánh chuỗi sau khi chuyển đổi về dạng chữ thường
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Address>> GetAllAddressAsync(bool trackChanges)
        {
            return await _dbContext.Addresses.ToListAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAddressAsync(Address address)
        {
            _dbContext.Addresses.Update(address);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAddress(Address address)
        {
            Delete(address);
        }
    }
}
