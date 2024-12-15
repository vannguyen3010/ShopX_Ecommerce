using Shared.DTO.CategoryIntroduce;
using Shared.DTO.CateProduct;
using Shared.DTO.Contact;
using Shared.DTO.Introduce;
using Shared.DTO.Order;
using Shared.DTO.Product;
using Shared.DTO.ShippingCost;

namespace Shared.DTO.Response
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
    public class ApiProductResponse<T, U>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public U Data2nd { get; set; }
    }
    public class ApiResponses
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ProductResponseDto Data { get; set; }
    }
    public class ProductResponseDto
    {
        public int TotalCount { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
    }
    public class CateProductResponseDto
    {
        public int TotalCount { get; set; }
        public IEnumerable<CateProductDto> Categories { get; set; }
    }
    public class IntroduceResponse
    {
        public int TotalCount { get; set; }
        public IEnumerable<IntroduceDto> Introduces { get; set; }
    }
    public class ContactResponse
    {
        public int totalCount { get; set; }
        public IEnumerable<ContactDto> contacts { get; set; }
    }
    public class IntroduceCategoryResponse
    {
        public int TotalCount { get; set; }
        public IEnumerable<CategoryIntroduceDto> categories { get; set; }
    }
    public class OrderResponse
    {
        public int TotalCount { get; set; }
        public IEnumerable<OrderDto> Orders { get; set; }
    }
    public class ShippingCostResponse
    {
        public int TotalCount { get; set; }
        public IEnumerable<ShippingCostDto> ShippingCosts { get; set; }
    }
}
