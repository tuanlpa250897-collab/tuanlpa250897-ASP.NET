using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ExpenseTracker.Models.ViewModels;
using ExpenseTracker.Services;

namespace ExpenseTracker.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly DataService _dataService;

        public DashboardController(DataService dataService)
        {
            _dataService = dataService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var transactions = _dataService.GetTransactions(userId);
            var categories = _dataService.GetCategories();

            var totalIncome = transactions.Where(t => t.Type == "income").Sum(t => t.Amount);
            var totalExpense = transactions.Where(t => t.Type == "expense").Sum(t => t.Amount);
            var balance = totalIncome - totalExpense;

            var recentTransactions = transactions
                .OrderByDescending(t => t.Date)
                .Take(5)
                .ToList();

            var viewModel = new DashboardViewModel
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                Balance = balance,
                TransactionCount = transactions.Count,
                RecentTransactions = recentTransactions,
                Categories = categories
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult GetChartData()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var transactions = _dataService.GetTransactions(userId);
            var categories = _dataService.GetCategories();

            // Expense chart data (last 7 days)
            var expenseData = new List<object>();
            for (int i = 6; i >= 0; i--)
            {
                var date = DateTime.Now.AddDays(-i);
                var dayExpenses = transactions
                    .Where(t => t.Type == "expense" && t.Date.Date == date.Date)
                    .Sum(t => t.Amount);
                expenseData.Add(new { date = date.ToString("dd/MM"), amount = dayExpenses });
            }

            // Category chart data
            var categoryData = categories
                .Where(c => c.Type == "expense")
                .Select(c => new
                {
                    name = c.Name,
                    value = transactions
                        .Where(t => t.CategoryId == c.Id && t.Type == "expense")
                        .Sum(t => t.Amount),
                    color = c.Color
                })
                .Where(c => c.value > 0)
                .ToList();

            return Json(new { expenseData, categoryData });
        }
    }
}