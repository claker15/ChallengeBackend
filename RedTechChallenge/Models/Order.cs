using System.ComponentModel.DataAnnotations;

namespace RedTechChallenge.Models
{
    public class Order
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(13)]
        public string Type { get; set; }
        [Required]
        [MaxLength(255)]
        public string CustomerName { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        [MaxLength(100)]
        public string CreatedByUsername { get; set; }

    }
}
