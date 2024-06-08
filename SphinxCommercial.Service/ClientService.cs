using AutoMapper;
using SphinxCommercial.Core.DataTransferObjects;
using SphinxCommercial.Core.Interfaces.Repositories;
using SphinxCommercial.Core.Interfaces.Services;
using SphinxCommercial.Core.Models;
using SphinxCommercial.Repository.Specifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphinxCommercial.Service
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddClient(ClientDto clientDto)
        {
            var Clients = await GetClientsSelectList();

            var clientsCodes = Clients.Select(c => c.Code).ToList();

            var client = _mapper.Map<Client>(clientDto);

            if (clientsCodes.Contains(client.Code))
            {
                throw new Exception("Can't Add Duplicate Code");
            }

            await _unitOfWork.Repository<Client,int>().AddAsync(client);

            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<Client>> GetClientsSelectList()
            => await _unitOfWork.Repository<Client, int>().GetAllAsync();


        public async Task<PaginatedResultDto<ClientDto>> GetClients(SpecificationParameters parameters)
        {
            var specs = new ClientSpecifications(parameters);
            var clients = await _unitOfWork.Repository<Client, int>().GetAllWithSpecsAsync(specs);

            var specsCount = new ClientCountSpecifications(parameters);
            var count = await _unitOfWork.Repository<Client, int>().CountAsync(specsCount);


            var mappedClients = _mapper.Map<IReadOnlyList<ClientDto>>(clients);

            return new PaginatedResultDto<ClientDto>
            {
                Data = mappedClients,
                PageIndex = parameters.PageIndex,
                PageSize = parameters.PageSize,
                TotalCount = count
            };
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Client, int>().GetAsync(id);
        }

        public async Task UpdateClient(Client client)
        {
            if (client == null)
            {
                throw new Exception("Client not found");
            }

            var dbClient = await GetClientByIdAsync(client.Id);

            if (dbClient == null)
            {
                throw new Exception("Client not found in the database");
            }

            client.Code = dbClient.Code;


            dbClient.Name = client.Name;
            dbClient.State = client.State;
            dbClient.Class = client.Class;

            _unitOfWork.Repository<Client, int>().Update(dbClient);

            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteClient(int id)
        {
            var client = await _unitOfWork.Repository<Client, int>().GetAsync(id);
            if (client != null)
            {
                _unitOfWork.Repository<Client, int>().Delete(client);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
