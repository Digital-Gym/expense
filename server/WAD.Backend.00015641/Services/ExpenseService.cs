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

    public async Task<Expense> CreateExpenseAsync(Expense expense)
    {
        _context.Expenses.Add(expense);
        await _context.SaveChangesAsync();
        return expense;
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
