using Microsoft.EntityFrameworkCore;
using ExpenseTracker.Models;

namespace ExpenseTracker.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ExpenseDbContext context)
        {
            context.Database.EnsureCreated();

            // Check if data already exists
            if (context.Categories.Any())
                return;

            // Seed Categories
            var categories = new Category[]
            {
                new Category { Id = "1", Name = "Ăn uống", Icon = "🍽️", Color = "#EF4444", Type = "expense" },
                new Category { Id = "2", Name = "Di chuyển", Icon = "🚗", Color = "#3B82F6", Type = "expense" },
                new Category { Id = "3", Name = "Giải trí", Icon = "🎮", Color = "#8B5CF6", Type = "expense" },
                new Category { Id = "4", Name = "Mua sắm", Icon = "🛒", Color = "#F59E0B", Type = "expense" },
                new Category { Id = "5", Name = "Sức khỏe", Icon = "💊", Color = "#10B981", Type = "expense" },
                new Category { Id = "6", Name = "Học tập", Icon = "📚", Color = "#6366F1", Type = "expense" },
                new Category { Id = "7", Name = "Lương", Icon = "💰", Color = "#22C55E", Type = "income" },
                new Category { Id = "8", Name = "Đầu tư", Icon = "📈", Color = "#059669", Type = "income" },
                new Category { Id = "9", Name = "Khác", Icon = "📝", Color = "#6B7280", Type = "expense" }
            };
            context.Categories.AddRange(categories);

            // Seed User
            var user = new User
            {
                Id = "1",
                Name = "Người dùng",
                Email = "user@example.com",
                Password = "123456",
                Role = "user",
                CreatedAt = DateTime.Now
            };
            context.Users.Add(user);

            // Seed Transactions
            var transactions = new Transaction[]
            {
                new Transaction { Id = "1", UserId = "1", CategoryId = "7", Amount = 15000000, Description = "Lương tháng 12", Date = new DateTime(2024, 12, 1), Type = "income" },
                new Transaction { Id = "2", UserId = "1", CategoryId = "1", Amount = 150000, Description = "Ăn trưa", Date = new DateTime(2024, 12, 15), Type = "expense" },
                new Transaction { Id = "3", UserId = "1", CategoryId = "2", Amount = 50000, Description = "Xe bus", Date = new DateTime(2024, 12, 15), Type = "expense" },
                new Transaction { Id = "4", UserId = "1", CategoryId = "3", Amount = 200000, Description = "Xem phim", Date = new DateTime(2024, 12, 14), Type = "expense" },
                new Transaction { Id = "5", UserId = "1", CategoryId = "4", Amount = 500000, Description = "Mua quần áo", Date = new DateTime(2024, 12, 13), Type = "expense" }
            };
            context.Transactions.AddRange(transactions);

            // Seed Budgets
            var budgets = new Budget[]
            {
                new Budget { Id = "1", UserId = "1", CategoryId = "1", Amount = 3000000, Period = "monthly", Spent = 1200000 },
                new Budget { Id = "2", UserId = "1", CategoryId = "2", Amount = 1000000, Period = "monthly", Spent = 800000 },
                new Budget { Id = "3", UserId = "1", CategoryId = "3", Amount = 2000000, Period = "monthly", Spent = 1500000 }
            };
            context.Budgets.AddRange(budgets);

            context.SaveChanges();
        }
    }
}