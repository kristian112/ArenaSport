using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArenaSportProj.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        [MaxLength(200)]
        public string Address { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string PaymentMethod { get; set; } = "Наложен платеж";

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        // One-to-many с OrderItem
        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
