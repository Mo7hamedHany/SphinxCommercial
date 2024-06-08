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
    public class ClientProductService : IClientProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClientProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddClientProduct(ClientProduct clientProduct)
        {
            var cp = _mapper.Map<ClientProduct>(clientProduct);

            var product = await _unitOfWork.Repository<Product, int>().GetAsync(cp.ProductId);

            if (!product.IsActive)
                throw new Exception("Can't add an InActive Product");

            await _unitOfWork.Repository<ClientProduct, int>().AddAsync(cp);

            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteClientProduct(int id)
        {
            var clientProduct = await _unitOfWork.Repository<ClientProduct, int>().GetAsync(id);
            if (clientProduct != null)
            {
                _unitOfWork.Repository<ClientProduct, int>().Delete(clientProduct);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<ClientProduct> GetClientProductById(int id)
        {
            var clientProduct = await _unitOfWork.Repository<ClientProduct, int>().GetAsync(id);

            if (clientProduct is null)
                throw new Exception("Product is not found");

            return clientProduct;
        }

        public async Task<IEnumerable<ClientProductToReturnDto>> GetClientsSelectList(int clientId)
        {
            var specs = new ClientProductSpecifications(clientId);
            var clients = await _unitOfWork.Repository<ClientProduct, int>().GetAllWithSpecsAsync(specs);

            return _mapper.Map<IEnumerable<ClientProductToReturnDto>>(clients);
        }

        public async Task<PaginatedResultDto<ClientProductToReturnDto>> GetProducts(SpecificationParameters parameters)
        {
            var specs = new ClientProductSpecifications(parameters);
            var clientProducts = await _unitOfWork.Repository<ClientProduct, int>().GetAllWithSpecsAsync(specs);

            var specsCount = new ClientProductCountSpecifications(parameters);
            var count = await _unitOfWork.Repository<ClientProduct, int>().CountAsync(specsCount);


            var mappedProducts = _mapper.Map<IReadOnlyList<ClientProductToReturnDto>>(clientProducts);

            return new PaginatedResultDto<ClientProductToReturnDto>
            {
                Data = mappedProducts,
                PageIndex = parameters.PageIndex,
                PageSize = parameters.PageSize,
                TotalCount = count
            };
        }

        public async Task UpdateClientProduct(ClientProduct clientProduct)
        {
            if (clientProduct == null)
            {
                throw new Exception("Client not found");
            }

            _unitOfWork.Repository<ClientProduct, int>().Update(clientProduct);

            await _unitOfWork.CompleteAsync();
        }
    }
}
