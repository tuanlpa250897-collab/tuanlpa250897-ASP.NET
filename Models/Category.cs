using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models
{
    public class Category
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        public string Icon { get; set; } = string.Empty;
        
        public string Color { get; set; } = string.Empty;
        
        [Required]
        public string Type { get; set; } = string.Empty; // "income" or "expense"
    }
}