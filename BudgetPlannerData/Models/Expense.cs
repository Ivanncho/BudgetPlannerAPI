namespace BudgetPlannerApi.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string Month { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Category { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public bool IsPlanned { get; set; }
    }
}
