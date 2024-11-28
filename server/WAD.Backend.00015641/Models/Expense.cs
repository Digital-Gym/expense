namespace WAD.Backend._00015641.Models
{
    public class Expense
    {
        public int ExpenseId { get; set; }
        public string Title { get; set; }
        public int Amount { get; set; }
        // user fk
        public int UserId { get; set; }
        public User User { get; set; }
        // category fk
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public DateTime Date { get; set; }
    }
}
