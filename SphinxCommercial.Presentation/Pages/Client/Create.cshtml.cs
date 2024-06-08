using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SphinxCommercial.Core.DataTransferObjects;
using SphinxCommercial.Core.Interfaces.Services;
using CoreClient = SphinxCommercial.Core.Models.Client; 
using System.Threading.Tasks;

namespace SphinxCommercial.Presentation.Pages.ClientPages
{
    public class CreateModel : PageModel
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        [BindProperty]
        public CoreClient Client { get; set; } 

        public CreateModel(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {

                foreach (var modelStateKey in ModelState.Keys)
                {
                    var value = ModelState[modelStateKey];
                    var errors = value.Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"Error in {modelStateKey}: {error.ErrorMessage}");
                    }
                }
                return Page(); 
            }

            var clientDto = _mapper.Map<ClientDto>(Client); 
            await _clientService.AddClient(clientDto);

            return RedirectToPage("./Index"); 
        }
    }
}
