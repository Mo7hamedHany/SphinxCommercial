using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SphinxCommercial.Core.DataTransferObjects;
using SphinxCommercial.Core.Interfaces.Services;
using System.Threading.Tasks;
using CoreClientProduct = SphinxCommercial.Core.Models.ClientProduct;

namespace SphinxCommercial.Presentation.Pages.ClientProduct
{
    public class CreateModel : PageModel
    {
        private readonly IClientProductService _clientProductService;
        private readonly IProductService _productService;
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        [BindProperty]
        public ClientProductToCreateDto ClientProduct { get; set; }
        public SelectList ClientList { get; set; }
        public SelectList ProductList { get; set; }

        public CreateModel(IClientProductService clientProductService, IMapper mapper, IProductService productService, IClientService clientService)
        {
            _clientProductService = clientProductService;
            _mapper = mapper;
            _productService = productService;
            _clientService = clientService;
        }

        public async Task OnGetAsync()
        {
            var clients = await _clientService.GetClientsSelectList();
            var products = await _productService.GetProductsSelectList();

            ClientList = new SelectList(clients, "Id", "Name");
            ProductList = new SelectList(products, "Id", "Name");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var clientProduct = _mapper.Map<CoreClientProduct>(ClientProduct);
            await _clientProductService.AddClientProduct(clientProduct);

            return RedirectToPage("./Index");
        }
    }
}
