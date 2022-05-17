using System.Text.Json.Serialization;

namespace ExpenseSystem.Models {
    public class ExpenseLine {

        public int Id { get; set; }
        public int Quantity { get; set; } = 0;
        public int ExpenseId { get; set; } = 0;
        [JsonIgnore]
        public virtual Expense? Expense { get; set; }
        public int ItemId { get; set; } = 0;
        public virtual Item? Item { get; set; }

    }
}
