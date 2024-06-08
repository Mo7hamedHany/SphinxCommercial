using SphinxCommercial.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphinxCommercial.Repository.Specifications
{
    public class ClientSpecifications : BaseSpecifications<Client>
    {
        public ClientSpecifications(SpecificationParameters parameters)
        {
            IncludeExpressions.Add(lc => lc.ClientProducts);

            ApplyPagination(parameters.PageSize, parameters.PageIndex);

            OrderBy = p => p.Code;
        }
    }
}
