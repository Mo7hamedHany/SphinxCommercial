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
    public interface IClientProductService
    {
        Task<PaginatedResultDto<ClientProductToReturnDto>> GetProducts(SpecificationParameters parameters);
        Task<IEnumerable<ClientProductToReturnDto>> GetClientsSelectList(int clientId);
        Task<ClientProduct> GetClientProductById(int id);
        Task AddClientProduct(ClientProduct clientProduct);
        Task UpdateClientProduct(ClientProduct clientProduct);
        Task DeleteClientProduct(int id);
    }
}
