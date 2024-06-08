using System.ComponentModel.DataAnnotations;

namespace SphinxCommercial.Core.Models
{
    public class ClientProduct : BaseModel<int>
    {
        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        [StringLength(255)]
        public string License { get; set; }

        [Required]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}