using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SphinxCommercial.Core.DataTransferObjects;
using SphinxCommercial.Core.Interfaces.Services;
using SphinxCommercial.Repository.Specifications;
using SphinxCommercial.Service;
using ProductCore = SphinxCommercial.Core.Models.Product;

namespace SphinxCommercial.Presentation.Pages.Product
{
    public class EditModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        [BindProperty]
        public ProductDto Product { get; set; }

        public EditModel(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {

            var pd = await _productService.GetProductById(id);

            Product = _mapper.Map<ProductDto>(pd);


            if (Product == null)
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

            await _productService.UpdateProduct(_mapper.Map<ProductCore>(Product));

            return RedirectToPage("./Index");
        }
    }
}
