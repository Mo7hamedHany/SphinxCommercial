using SphinxCommercial.Core.DataTransferObjects;
using SphinxCommercial.Core.Models;
using SphinxCommercial.Repository.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphinxCommercial.Core.Interfaces.Services
{
    public interface IProductService
    {
        Task<PaginatedResultDto<ProductDto>> GetProducts(SpecificationParameters parameters);
        Task<Product> GetProductById(int id);
        Task<IEnumerable<Product>> GetProductsSelectList();
        Task AddProduct(ProductDto productDto);
        Task UpdateProduct(Product product);
        Task DeleteProduct(int id);
    }
}
