using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SphinxCommercial.Core.DataTransferObjects;
using SphinxCommercial.Core.Interfaces.Services;
using SphinxCommercial.Core.Models;
using SphinxCommercial.Repository.Specifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SphinxCommercial.Presentation.Pages.Client
{
    public class IndexModel : PageModel
    {
        private readonly IClientService _clientService;

        public IndexModel(IClientService clientService)
        {
            _clientService = clientService;
        }

        public PaginatedResultDto<ClientDto> Clients { get; set; }

        public async Task<IActionResult> OnGetAsync([FromQuery] SpecificationParameters parameters)
        {
            Clients = await _clientService.GetClients(parameters);
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _clientService.DeleteClient(id);
            return RedirectToPage(); 
        }
    }
}
