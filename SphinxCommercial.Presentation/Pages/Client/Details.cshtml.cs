using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SphinxCommercial.Core.Interfaces.Services;
using CoreClient = SphinxCommercial.Core.Models.Client;
using SphinxCommercial.Core.Models;
using System.Threading.Tasks;
using SphinxCommercial.Core.DataTransferObjects;
using AutoMapper;

namespace SphinxCommercial.Presentation.Pages.Client
{
    public class DetailsModel : PageModel
    {
        private readonly IClientService _clientService;
        private readonly IClientProductService _clientProductService;
        private readonly IMapper _mapper;

        public DetailsModel(IClientService clientService, IMapper mapper, IClientProductService clientProductService)
        {
            _clientService = clientService;
            _mapper = mapper;
            _clientProductService = clientProductService;
        }

        [BindProperty]
        public ClientDto Client { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            var relatedClientProducts = await _clientProductService.GetClientsSelectList(id);

            Client = _mapper.Map<ClientDto>(client);

            if (Client == null)
            {
                return NotFound();
            }

            foreach (var _ in Client.ClientProducts!)
            {
                Client.ClientProducts = relatedClientProducts.ToList();
            }


            return Page();
        }
    }
}
