using Microsoft.EntityFrameworkCore;
using WAD.Backend._00015641.Data;
using WAD.Backend._00015641.Models;
using WAD.Backend._00015641.Services;

public class ExpenseService : IExpenseService
{
    private readonly AppDbContext _context;

    public ExpenseService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ExpenseDto>> GetAllExpensesAsync(DateTime? startDate, DateTime? finishDate)
    {
        var query = _context.Expenses
            .Include(e => e.Category)
            .AsQueryable();

        if (startDate.HasValue)
        {
            query = query.Where(e => e.Date >= startDate.Value);
        }

        if (finishDate.HasValue)
        {
            query = query.Where(e => e.Date <= finishDate.Value);
        }

        return await query
            .Select(e => new ExpenseDto
            {
                ExpenseId = e.ExpenseId,
                Title = e.Title,
                Amount = e.Amount,
                CategoryName = e.Category.Name,
                CategoryLink = e.Category.Icon,
                Date = e.Date
            })
            .ToListAsync();
    }

    public async Task<(List<string> labels, List<int> dataset)> GetExpensesStatsAsync(DateTime startDate, DateTime finishDate)
    {
        // Filter, group, and aggregate data
        var stats = await _context.Expenses
            .Include(e => e.Category)
            .Where(e => e.Date >= startDate && e.Date <= finishDate) // Filter by date range
            .GroupBy(e => e.Category.Name) // Group by category name
            .Select(g => new
            {
                Category = g.Key, // Category name
                TotalAmount = g.Sum(e => e.Amount) // Sum of amounts in this category
            })
            .ToListAsync();

        // Separate into labels and dataset
        var labels = stats.Select(s => s.Category).ToList();
        var dataset = stats.Select(s => s.TotalAmount).ToList();

        return (labels, dataset);
    }

    public async Task<Expense> GetExpenseByIdAsync(int id)
    {
        return await _context.Expenses.FindAsync(id);
    }

    public async Task<Expense> CreateExpenseAsync(RawExpense expense)
    {
        if (expense.UserId <= 0 || expense.CategoryId <= 0)
        {
            throw new ArgumentException("UserId and CategoryId must be valid.");
        }

        // shitty code
        var user = await _context.Users.FindAsync(expense.UserId) ?? throw new KeyNotFoundException($"User with ID {expense.UserId} not found.");
        var category = await _context.Categories.FindAsync(expense.CategoryId) ?? throw new KeyNotFoundException($"Category with ID {expense.CategoryId} not found.");

        var expenseInstance = expense.getExpenseInstance(user, category);


        // save
        _context.Expenses.Add(expenseInstance);
        await _context.SaveChangesAsync();

        return expenseInstance;
    }

    public async Task<bool> DeleteExpenseAsync(int id)
    {
        var expense = await _context.Expenses.FindAsync(id);
        if (expense == null)
        {
            return false;
        }

        _context.Expenses.Remove(expense);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<ExpenseDto> UpdateExpenseAsync(int id, ExpenseUpdateDto updateDto)
    {
        var expense = await _context.Expenses.FindAsync(id);

        if (expense == null)
        {
            throw new KeyNotFoundException($"Expense with id {id} not found.");
        }

        // Update only provided fields
        if (!string.IsNullOrEmpty(updateDto.Title))
        {
            expense.Title = updateDto.Title;
        }

        if (updateDto.Amount.HasValue)
        {
            expense.Amount = updateDto.Amount.Value;
        }

        // validation
        if (updateDto.CategoryId.HasValue)
        {
            var categoryExists = await _context.Categories.AnyAsync(c => c.CategoryId == updateDto.CategoryId.Value);
            if (!categoryExists)
            {
                throw new ArgumentException($"Category with id {updateDto.CategoryId} does not exist.");
            }

            expense.CategoryId = updateDto.CategoryId.Value;
        }

        await _context.SaveChangesAsync();

        return new ExpenseDto
        {
            ExpenseId = expense.ExpenseId,
            Title = expense.Title,
            Amount = expense.Amount,
            CategoryName = (await _context.Categories.FindAsync(expense.CategoryId))?.Name,
            CategoryLink = (await _context.Categories.FindAsync(expense.CategoryId))?.Icon,
            Date = expense.Date
        };
    }
}
