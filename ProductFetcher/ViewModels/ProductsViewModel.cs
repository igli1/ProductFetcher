namespace ProductFetcher.ViewModels;

public class ProductsViewModel
{
    public bool Status { get; set; }
    public IEnumerable<ProductDetailsViewModel> Products { get; set; }
}