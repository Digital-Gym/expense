namespace WAD.Backend._00015641.Services
{
    public class ExpenseDto
    {
        public int ExpenseId { get; set; }
        public string Title { get; set; }
        public int Amount { get; set; }
        public string CategoryName { get; set; }
        public string CategoryLink { get; set; }
        public DateTime Date { get; set; }
    }
}