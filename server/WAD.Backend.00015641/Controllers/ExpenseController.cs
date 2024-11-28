using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WAD.Backend._00015641.Data;
using WAD.Backend._00015641.Models;

namespace WAD.Backend._00015641.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExpenseController : ControllerBase
    {
        private readonly ILogger<ExpenseController> _logger;
        private readonly AppDbContext _context;

        public ExpenseController(ILogger<ExpenseController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetExpenses()
        {
            var expenses = await _context.Expenses.ToListAsync();
            return Ok(expenses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                _logger.LogWarning("Expense with id {Id} not found", id);
                return NotFound();
            }
            return Ok(expense);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExpense([FromBody] Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetExpense), new { id = expense.ExpenseId }, expense);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                _logger.LogWarning("Expense with id {Id} not found", id);
                return NotFound();
            }
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
