using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Data
{
    public static class DatabaseInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ExpenseDbContext>();
            
            // Ensure database is created
            context.Database.EnsureCreated();
        }
    }
}