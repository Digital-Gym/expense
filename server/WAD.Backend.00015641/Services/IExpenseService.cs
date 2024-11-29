using WAD.Backend._00015641.Models;

namespace WAD.Backend._00015641.Services
{
    public interface IExpenseService
    {
        Task<IEnumerable<ExpenseDto>> GetAllExpensesAsync(DateTime? startDate, DateTime? finishDate);
        Task<Expense> GetExpenseByIdAsync(int id);
        Task<(List<string> labels, List<int> dataset)> GetExpensesStatsAsync(DateTime startDate, DateTime finishDate);
        Task<Expense> CreateExpenseAsync(RawExpense expense);
        Task<bool> DeleteExpenseAsync(int id);
        Task<ExpenseDto> UpdateExpenseAsync(int id, ExpenseUpdateDto updateDto);
    }
}
