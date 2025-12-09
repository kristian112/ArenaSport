using System.ComponentModel.DataAnnotations;

namespace ArenaSportProj.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        // One-to-many с Product
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
