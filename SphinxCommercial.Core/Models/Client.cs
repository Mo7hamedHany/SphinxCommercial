using SphinxCommercial.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SphinxCommercial.Core.Models
{
    public class Client : BaseModel<int>
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(9)]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Code must be 9 digits.")]
        public string Code { get; set; }

        [Required]
        public ClientClass Class { get; set; }

        [Required]
        public ClientState State { get; set; }

        public List<ClientProduct>? ClientProducts { get; set; }

    }
}
