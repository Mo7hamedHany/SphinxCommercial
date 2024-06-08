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
    public interface IClientService
    {
        Task<PaginatedResultDto<ClientDto>> GetClients(SpecificationParameters parameters);
        Task<IEnumerable<Client>> GetClientsSelectList();
        Task<Client> GetClientByIdAsync(int id);
        Task AddClient(ClientDto clientDto);
        Task UpdateClient(Client client);
        Task DeleteClient(int id);
    }
}
