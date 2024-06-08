using BudgetPlannerApi.Models;
using BudgetPlannerData.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BudgetPlannerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ControllerBase
    {
        private readonly BudgetContext _budgetContext;

        public IncomeController(BudgetContext budgetContext)
        {
            _budgetContext = budgetContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Income>>> GetIncomes()
        {
            return await _budgetContext.Incomes.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Income>> AddIncome(Income income)
        {
            if (income == null)
            {
                return BadRequest("Enter income");
            }
            _budgetContext.Incomes.Add(income);
            await _budgetContext.SaveChangesAsync();
            return Ok("Income added.");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Income>> UpdateIncome(Income income, int id)
        {
            if (income == null)
            {
                return BadRequest("Enter income");
            }
            
            var incomeToUpdate = _budgetContext.Incomes.FirstOrDefault(x => x.Id == id);
            if (incomeToUpdate == null)
            {
                return NotFound("Income not found");
            }
            incomeToUpdate.Month = income.Month;
            incomeToUpdate.Year = income.Year;
            incomeToUpdate.Category = income.Category;
            incomeToUpdate.Amount = income.Amount;
            incomeToUpdate.IsPlanned = income.IsPlanned;
            
            _budgetContext.Incomes.Update(incomeToUpdate);
            await _budgetContext.SaveChangesAsync();
            return Ok("Income updated");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteIncome(int id)
        {
            var incomeToDelete = await _budgetContext.Incomes.FindAsync(id);
            if (incomeToDelete == null)
            {
                return NotFound("Income not found");
            }
            _budgetContext.Incomes.Remove(incomeToDelete);
            await _budgetContext.SaveChangesAsync();
            return Ok("Income deleted.");
        }
    }
}
