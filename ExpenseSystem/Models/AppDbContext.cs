using Microsoft.EntityFrameworkCore;

namespace ExpenseSystem.Models {
    public class AppDbContext : DbContext {
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Expense> Expenses { get; set; } = null!;
        public virtual DbSet<ExpenseLine> ExpenseLines { get; set; } = null!;
        public virtual DbSet<Item> Items { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) { }

    }
}
