using WAD.Backend._00015641.Models;

namespace WAD.Backend._00015641.Services
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expense>> GetAllExpensesAsync();
        Task<Expense> GetExpenseByIdAsync(int id);
        Task<Expense> CreateExpenseAsync(Expense expense);
        Task<bool> DeleteExpenseAsync(int id);
    }
}
