using ProductFetcher.Dto.Service;

namespace ProductFetcher.Service;

public interface IProductService
{
    Task<ServiceResponse<IEnumerable<Product>>> GetAllProductsAsync();
}