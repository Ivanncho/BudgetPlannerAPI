using BudgetPlannerApi.Models;
using BudgetPlannerData.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BudgetPlannerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly BudgetContext _budgetContext;
        public ExpenseController(BudgetContext budgetContext)
        {
            _budgetContext = budgetContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            return await _budgetContext.Expenses.ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<Expense>> AddExpense(Expense expense)
        {
            _budgetContext.Expenses.Add(expense);
            await _budgetContext.SaveChangesAsync();
            return Ok("Expense added.");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Expense>> UpdateIncome(Expense expense, int id)
        {
            if (expense == null)
            {
                return BadRequest("Enter income");
            }

            var expenseToUpdate = _budgetContext.Expenses.FirstOrDefault(x => x.Id == id);
            if (expenseToUpdate == null)
            {
                return NotFound("Income not found");
            }
            expenseToUpdate.Month = expense.Month;
            expenseToUpdate.Year = expense.Year;
            expenseToUpdate.Category = expense.Category;
            expenseToUpdate.Amount = expense.Amount;
            expenseToUpdate.IsPlanned = expense.IsPlanned;

            _budgetContext.Expenses.Update(expenseToUpdate);
            await _budgetContext.SaveChangesAsync();
            return Ok("Income updated");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteIncome(int id)
        {
            var expenseToDelete = await _budgetContext.Expenses.FindAsync(id);
            if (expenseToDelete == null)
            {
                return NotFound("Income not found");
            }
            _budgetContext.Expenses.Remove(expenseToDelete);
            await _budgetContext.SaveChangesAsync();
            return Ok("Income deleted.");
        }
    }
}
