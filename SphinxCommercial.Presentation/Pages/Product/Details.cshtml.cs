using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SphinxCommercial.Core.DataTransferObjects;
using SphinxCommercial.Core.Interfaces.Services;
using SphinxCommercial.Service;

namespace SphinxCommercial.Presentation.Pages.Product
{
    public class DetailsModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        [BindProperty]
        public ProductDto Product { get; set; }

        public DetailsModel(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var product = await _productService.GetProductById(id);

            Product = _mapper.Map<ProductDto>(product);

            return Page();
        }
    }
}
