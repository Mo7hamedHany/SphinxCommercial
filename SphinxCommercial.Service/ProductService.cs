using AutoMapper;
using SphinxCommercial.Core.DataTransferObjects;
using SphinxCommercial.Core.Interfaces.Repositories;
using SphinxCommercial.Core.Interfaces.Services;
using SphinxCommercial.Core.Models;
using SphinxCommercial.Repository.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphinxCommercial.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PaginatedResultDto<ProductDto>> GetProducts(SpecificationParameters parameters)
        {
            var specs = new ProductSpecifications(parameters);
            var products = await _unitOfWork.Repository<Product, int>().GetAllWithSpecsAsync(specs);

            var specsCount = new ProductCountSpecifications(parameters);
            var count = await _unitOfWork.Repository<Product, int>().CountAsync(specsCount);


            var mappedProducts = _mapper.Map<IReadOnlyList<ProductDto>>(products);

            return new PaginatedResultDto<ProductDto>
            {
                Data = mappedProducts,
                PageIndex = parameters.PageIndex,
                PageSize = parameters.PageSize,
                TotalCount = count
            };
        }

        public async Task AddProduct(ProductDto productDto)
        {

            var product = _mapper.Map<Product>(productDto);

            await _unitOfWork.Repository<Product, int>().AddAsync(product);

            // Save changes to the database
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteProduct(int id)
        {
            var specs = new ProductSpecifications(id);

            var product = await _unitOfWork.Repository<Product,int>().GetWithSpecsAsync(specs);

            if (product != null)
            {
                if (product.ClientProducts.Any())
                    throw new Exception("The Product is attached to a client");

                 _unitOfWork.Repository<Product,int>().Delete(product);

                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<Product> GetProductById(int id)
        {
            var product = await _unitOfWork.Repository<Product,int>().GetAsync(id);

            if (product is null)
                throw new Exception("Product is not found");

            return product;
        }

        public async Task UpdateProduct(Product product)
        {
            if (product is null)
                throw new Exception("Product Not Found");

            _unitOfWork.Repository<Product,int>().Update(product);

            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsSelectList()
        {
           var allProducts = await _unitOfWork.Repository<Product, int>().GetAllAsync();

            var activeProducts = allProducts.Where(x => x.IsActive == true);

            return activeProducts;
        }

    }
}
