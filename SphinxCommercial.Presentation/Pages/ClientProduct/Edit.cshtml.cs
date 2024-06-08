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
    public class EditModel : PageModel
    {
        private readonly IClientProductService _clientProductService;
        private readonly IProductService _productService;
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public EditModel(IClientProductService clientProductService, IProductService productService, IClientService clientService, IMapper mapper)
        {
            _clientProductService = clientProductService;
            _productService = productService;
            _clientService = clientService;
            _mapper = mapper;
        }

        [BindProperty]
        public ClientProductToCreateDto ClientProduct { get; set; }
        public SelectList ClientList { get; set; }
        public SelectList ProductList { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var clientProduct = await _clientProductService.GetClientProductById(id);
            if (clientProduct == null)
            {
                return NotFound();
            }

            ClientProduct = _mapper.Map<ClientProductToCreateDto>(clientProduct);

            var clients = await _clientService.GetClientsSelectList();
            var products = await _productService.GetProductsSelectList();

            ClientList = new SelectList(clients, "Id", "Name");
            ProductList = new SelectList(products, "Id", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                var clients = await _clientService.GetClientsSelectList();
                var products = await _productService.GetProductsSelectList();

                ClientList = new SelectList(clients, "Id", "Name");
                ProductList = new SelectList(products, "Id", "Name");

                return Page();
            }

            var clientProduct = _mapper.Map<CoreClientProduct>(ClientProduct);

            await _clientProductService.UpdateClientProduct(clientProduct);

            return RedirectToPage("./Index");
        }
    }
}
