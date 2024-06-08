using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SphinxCommercial.Core.DataTransferObjects;
using SphinxCommercial.Core.Interfaces.Services;
using SphinxCommercial.Repository.Specifications;
using SphinxCommercial.Service;

namespace SphinxCommercial.Presentation.Pages.ClientProductPages
{
    public class IndexModel : PageModel
    {
        private readonly IClientProductService _clientProductService;

        public IndexModel(IClientProductService clientProductService)
        {
            _clientProductService = clientProductService;
        }

        public PaginatedResultDto<ClientProductToReturnDto> ClientProducts { get; set; }

        public async Task<IActionResult> OnGetAsync([FromQuery] SpecificationParameters parameters)
        {
            ClientProducts = await _clientProductService.GetProducts(parameters);
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _clientProductService.DeleteClientProduct(id);
            return RedirectToPage();
        }
    }

}
