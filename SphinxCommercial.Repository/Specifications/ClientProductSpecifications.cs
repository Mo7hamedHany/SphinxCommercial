using SphinxCommercial.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SphinxCommercial.Repository.Specifications
{
    public class ClientProductSpecifications : BaseSpecifications<ClientProduct>
    {
        public ClientProductSpecifications(SpecificationParameters parameters) 
            : base(cp => 
                        (!parameters.ProductId.HasValue || parameters.ProductId == cp.ProductId) &&
                        (!parameters.ClientId.HasValue || parameters.ClientId == cp.ClientId))
        {
            IncludeExpressions.Add(cp => cp.Client);
            IncludeExpressions.Add(cp => cp.Product);

            ApplyPagination(parameters.PageSize, parameters.PageIndex);

            OrderBy = p => p.Product.Name;

        }

        public ClientProductSpecifications(int clientId)
                        : base(cp => cp.ClientId == clientId)
        {
            IncludeExpressions.Add(cp => cp.Client);
            IncludeExpressions.Add(cp => cp.Product);

            OrderBy = p => p.Product.Name;
        }
    }
}
