namespace ExpenseTracker.Models.ViewModels
{
    public class DashboardViewModel
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Balance { get; set; }
        public int TransactionCount { get; set; }
        public List<Transaction> RecentTransactions { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public string IncomeChange { get; set; } = "+3.2%";
        public string ExpenseChange { get; set; } = "-1.8%";
        public string SavingsGoal { get; set; } = "65%";
    }
}