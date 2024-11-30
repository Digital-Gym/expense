using WAD.Backend._00015641.Models;

namespace WAD.Backend._00015641.Services
{
    public class RawExpense
    {
        public int ExpenseId { get; set; }
        public string Title { get; set; }
        public int Amount { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public DateTime Date { get; set; }

        public Expense getExpenseInstance(User user, Category category)
        {
            return new Expense
            {
                ExpenseId = ExpenseId,
                Title = Title,
                Amount = Amount,
                UserId = UserId,
                Date = Date,
                User = user,
                Category = category
            };
        }
    }
}
