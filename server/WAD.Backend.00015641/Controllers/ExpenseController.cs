using Microsoft.AspNetCore.Mvc;
using WAD.Backend._00015641.Models;
using WAD.Backend._00015641.Services;

namespace WAD.Backend._00015641.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly ILogger<ExpenseController> _logger;
        private readonly IExpenseService _expenseService;

        public ExpenseController(ILogger<ExpenseController> logger, IExpenseService expenseService)
        {
            _logger = logger;
            _expenseService = expenseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetExpenses()
        {
            var expenses = await _expenseService.GetAllExpensesAsync();
            return Ok(expenses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpense(int id)
        {
            var expense = await _expenseService.GetExpenseByIdAsync(id);
            if (expense == null)
            {
                _logger.LogWarning("Expense with id {Id} not found", id);
                return NotFound();
            }
            return Ok(expense);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] RawExpense expense)
        {
            try
            {
                var createdExpense = await _expenseService.CreateExpenseAsync(expense);
                return Ok(CreatedAtAction(nameof(GetExpense), new { id = createdExpense.ExpenseId }, createdExpense));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the expense.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var success = await _expenseService.DeleteExpenseAsync(id);
            if (!success)
            {
                _logger.LogWarning("Expense with id {Id} not found", id);
                return NotFound();
            }
            return NoContent();
        }
    }
}
