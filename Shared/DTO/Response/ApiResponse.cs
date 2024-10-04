using Shared.DTO.Product;

namespace Shared.DTO.Response
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
    public class ProductResponseDto
    {
        public int TotalCount { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
