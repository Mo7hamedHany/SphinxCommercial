using SphinxCommercial.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SphinxCommercial.Repository.Specifications
{
    public class ProductSpecifications : BaseSpecifications<Product>
    {
        public ProductSpecifications(SpecificationParameters parameters)
        {
            ApplyPagination(parameters.PageSize, parameters.PageIndex);

            OrderBy = p => p.Name;
        }

        public ProductSpecifications(int id) : base( p => p.Id == id)
        {
            IncludeExpressions.Add(x => x.ClientProducts);
            OrderBy = p => p.Name;
        }
    }
}
