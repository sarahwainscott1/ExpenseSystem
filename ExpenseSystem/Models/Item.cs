using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseSystem.Models {
    public class Item {

        public int Id { get; set; }
        [StringLength(30)]
        public string Name { get; set; } = String.Empty;
        [Column(TypeName = "decimal(9,2)")]
        public decimal Price { get; set; } = 0;
    }
}
