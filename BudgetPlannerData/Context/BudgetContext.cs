
using BudgetPlannerApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetPlannerData.Context
{
    public class BudgetContext : DbContext
    {
        public BudgetContext(DbContextOptions<BudgetContext> options) :base(options) { }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Income> Incomes { get; set; }
    }
}
