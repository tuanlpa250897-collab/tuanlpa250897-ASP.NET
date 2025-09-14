using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Models;
using ExpenseTracker.Data;

namespace ExpenseTracker.Services
{
    public class DataService
    {
        private readonly ExpenseDbContext _context;

        public DataService(ExpenseDbContext context)
        {
            _context = context;
        }

        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }
        
        public List<Transaction> GetTransactions(string? userId = null)
        {
            var query = _context.Transactions.AsQueryable();
            
            if (userId != null)
                query = query.Where(t => t.UserId == userId);
                
            var transactions = query.ToList();
            
            foreach (var transaction in transactions)
            {
                transaction.Category = _context.Categories.FirstOrDefault(c => c.Id == transaction.CategoryId);
            }
            
            return transactions;
        }
        
        public List<Budget> GetBudgets(string? userId = null)
        {
            var query = _context.Budgets.AsQueryable();
            
            if (userId != null)
                query = query.Where(b => b.UserId == userId);
                
            var budgets = query.ToList();
            
            foreach (var budget in budgets)
            {
                budget.Category = _context.Categories.FirstOrDefault(c => c.Id == budget.CategoryId);
            }
            
            return budgets;
        }
        
        public User? GetUser(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }
        
        public bool CreateUser(User user)
        {
            if (_context.Users.Any(u => u.Email == user.Email))
                return false;
            
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }
        
        public void AddTransaction(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }
        
        public Category? GetCategory(string id)
        {
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }
    }
}