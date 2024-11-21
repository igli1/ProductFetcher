using System.Text.Json;
using Microsoft.Extensions.Options;
using ProductFetcher.Config;
using ProductFetcher.Dto.Service;

namespace ProductFetcher.Service;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    private ProductsApiConfig _productsApi;
    
    public ProductService(HttpClient httpClient, IOptions<ProductsApiConfig> apiOptions)
    {
        _httpClient = httpClient;
        _productsApi = apiOptions.Value;
    }

    public async Task<ServiceResponse<IEnumerable<Product>>> GetAllProductsAsync()
    {
        var allProducts = new List<Product>();
        var response = new ServiceResponse<IEnumerable<Product>>();
        try
        {
            string currentPageUrl = string.Concat(_productsApi.ApiUrl,"products");
            bool hasNext = true;

            while (hasNext)
            {
                var apiResponse = await _httpClient.GetAsync(currentPageUrl);
                
                if (!apiResponse.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Failed to fetch products: {apiResponse.ReasonPhrase}");
                }
                var json = await apiResponse.Content.ReadAsStringAsync();
                
                var productsResponse = JsonSerializer.Deserialize<ProductsResponse>(json);

                if (productsResponse?.Products != null)
                {
                    allProducts.AddRange(productsResponse.Products);
                }
                
                if (productsResponse?.Skip + productsResponse?.Limit < productsResponse?.Total)
                {
                    currentPageUrl = string.Concat(_productsApi.ApiUrl,"products?skip=", productsResponse.Skip + productsResponse.Limit, "&limit=",productsResponse.Limit );
                }
                else
                {
                    hasNext = false;
                }
            }
            response.Status = true;
            response.Data = allProducts;
            return response;
        }
        catch (Exception ex)
        {
            response.Status = false;
            response.Message = ex.Message;
            response.Data = new List<Product>();
            return response;
        }
    }

}