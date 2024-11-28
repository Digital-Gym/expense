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

    public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
    {
        return await _context.Expenses.ToListAsync();
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
}
