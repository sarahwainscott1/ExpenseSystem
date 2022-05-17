using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ExpenseSystem.Models {
    public class Expense {

        public int Id { get; set; }
        [StringLength(30)]
        public string Description { get; set; } = string.Empty;
        [StringLength(15)]
        public string Status { get; set; } = string.Empty;
        [Column(TypeName = "decimal(9,2)")]
        public decimal Total { get; set; } = 0;

        public int? EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }
        public virtual List<ExpenseLine> ExpenseLines { get; set; } = new List<ExpenseLine>();
        [JsonIgnore]
        public virtual Item? Item { get; set; }

    }
}
