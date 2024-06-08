using SphinxCommercial.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphinxCommercial.Repository.Specifications
{
    public class ClientProductCountSpecifications : BaseSpecifications<ClientProduct>
    {
        public ClientProductCountSpecifications(SpecificationParameters parameters)
    : base(cp =>
                (!parameters.ProductId.HasValue || parameters.ProductId == cp.ProductId) &&
                (!parameters.ClientId.HasValue || parameters.ClientId == cp.ClientId))
        {

        }
    }
}
