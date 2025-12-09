using System.ComponentModel.DataAnnotations;

namespace ArenaSportProj.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [Range(1, 50)]
        public int Quantity { get; set; } = 1;
    }
}
