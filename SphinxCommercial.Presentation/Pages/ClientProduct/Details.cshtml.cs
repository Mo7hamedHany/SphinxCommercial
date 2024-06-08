using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SphinxCommercial.Core.DataTransferObjects;
using SphinxCommercial.Core.Interfaces.Services;
using SphinxCommercial.Service;

namespace SphinxCommercial.Presentation.Pages.ClientProduct
{
    public class DetailsModel : PageModel
    {
        private readonly IClientProductService _clientProductService;
        private readonly IProductService _productService;
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        [BindProperty]
        public ClientProductToReturnDto ClientProduct { get; set; }

        public DetailsModel(IClientProductService clientProductService, IMapper mapper, IProductService productService, IClientService clientService)
        {
            _clientProductService = clientProductService;
            _mapper = mapper;
            _productService = productService;
            _clientService = clientService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var clientProduct = await _clientProductService.GetClientProductById(id);
            var client = await _clientService.GetClientByIdAsync(id);
            var clientName = client.Name;
            var product = await _productService.GetProductById(id);
            var productName = product.Name;

            ClientProduct = _mapper.Map<ClientProductToReturnDto>(clientProduct);

            ClientProduct.ProductName = productName;
            ClientProduct.ClientName = clientName;

            return Page();
        }
    }
}
