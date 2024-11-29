using Microsoft.AspNetCore.Mvc;
using WAD.Backend._00015641.Services;

namespace WAD.Backend._00015641.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly ILogger<ExpenseController> _logger;
        private readonly IExpenseService _expenseService;

        public ExpenseController(ILogger<ExpenseController> logger, IExpenseService expenseService)
        {
            _logger = logger;
            _expenseService = expenseService;
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetAllExpenses([FromQuery] DateTime? startDate, [FromQuery] DateTime? finishDate)
        {
            var expenses = await _expenseService.GetAllExpensesAsync(startDate, finishDate);
            return Ok(expenses);
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats([FromQuery] DateTime startDate, [FromQuery] DateTime finishDate)
        {
            // Validate the date range
            if (finishDate <= startDate)
            {
                return BadRequest("Finish date must be greater than start date.");
            }

            // Fetch stats using service
            var (labels, dataset) = await _expenseService.GetExpensesStatsAsync(startDate, finishDate);

            // Create the response
            var response = new
            {
                labels,
                dataset
            };

            return Ok(response);
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
