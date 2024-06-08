using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SphinxCommercial.Core.DataTransferObjects;
using SphinxCommercial.Core.Interfaces.Services;
using SphinxCommercial.Core.Models;
using CoreClient = SphinxCommercial.Core.Models.Client;

namespace SphinxCommercial.Presentation.Pages.Client
{
    public class EditModel : PageModel
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        [BindProperty]
        public ClientDto Client { get; set; }

        public EditModel(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var cl = await _clientService.GetClientByIdAsync(id);

            Client = _mapper.Map<ClientDto>(cl);
            

            if (Client == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _mapper.Map<CoreClient>(Client);
            await _clientService.UpdateClient(client);

            return RedirectToPage("./Index");
        }




    }
}
