using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SphinxCommercial.Core.DataTransferObjects;
using SphinxCommercial.Core.Interfaces.Services;
using SphinxCommercial.Core.Models;
using SphinxCommercial.Repository.Specifications;
using SphinxCommercial.Service;

namespace SphinxCommercial.Presentation.Pages.Product
{
    public class IndexModel : PageModel
    {

        private readonly IProductService _productService;

        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }

        public PaginatedResultDto<ProductDto> Products { get; set; }

        public async Task<IActionResult> OnGetAsync([FromQuery] SpecificationParameters parameters)
        {
            Products = await _productService.GetProducts(parameters);
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            await _productService.DeleteProduct(id);
            return RedirectToPage();
        }
    }
}
