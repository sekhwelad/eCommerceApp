using eShopper.Core.Entities;
namespace eShopper.Core.Interfaces
{
    public interface IProductRepository 
    {
        Task<Product> GetProductByIdAync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    }
}
