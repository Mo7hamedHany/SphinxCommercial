using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SphinxCommercial.Core.DataTransferObjects;
using SphinxCommercial.Core.Interfaces.Services;
using CoreProduct = SphinxCommercial.Core.Models.Product;
using SphinxCommercial.Core.Models;

namespace SphinxCommercial.Presentation.Pages.Product
{
    public class CreateModel : PageModel
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        [BindProperty]
        public CoreProduct Product { get; set; } = new CoreProduct(); 

        public CreateModel(IProductService productService, IMapper mapper)
        {
            _productService = productService;
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

            var productDto = _mapper.Map<ProductDto>(Product);
            await _productService.AddProduct(productDto);

            return RedirectToPage("./Index");
        }
    }
}
