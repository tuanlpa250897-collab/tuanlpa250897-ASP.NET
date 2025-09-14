using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ExpenseTracker.Models;
using ExpenseTracker.Services;

namespace ExpenseTracker.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly DataService _dataService;

        public TransactionController(DataService dataService)
        {
            _dataService = dataService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var transactions = _dataService.GetTransactions(userId)
                .OrderByDescending(t => t.Date)
                .ToList();

            return View(transactions);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = _dataService.GetCategories();
            return View();
        }

        [HttpPost]
        public IActionResult Add(Transaction transaction)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _dataService.GetCategories();
                return View(transaction);
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            transaction.UserId = userId ?? "1";
            transaction.Id = Guid.NewGuid().ToString();

            _dataService.AddTransaction(transaction);

            TempData["Success"] = "Giao dịch đã được thêm thành công!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetTransactionData()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var transactions = _dataService.GetTransactions(userId);

            var data = transactions.Select(t => new
            {
                id = t.Id,
                description = t.Description,
                amount = t.Amount,
                type = t.Type,
                date = t.Date.ToString("dd/MM/yyyy"),
                category = t.Category?.Name,
                categoryIcon = t.Category?.Icon,
                categoryColor = t.Category?.Color
            }).ToList();

            return Json(data);
        }
    }
}