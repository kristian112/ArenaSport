using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArenaSportProj.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string Brand { get; set; } = null!; // 🥊 Filter target

        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, 9999)]
        public decimal Price { get; set; } // 💸 Range filter

        public string? ImageUrl { get; set; }

        // FK към Category (спорт)
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
