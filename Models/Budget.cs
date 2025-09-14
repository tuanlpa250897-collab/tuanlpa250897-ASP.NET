using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models
{
    public class Budget
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [Required]
        public string UserId { get; set; } = string.Empty;
        
        [Required]
        public string CategoryId { get; set; } = string.Empty;
        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Số tiền phải lớn hơn 0")]
        public decimal Amount { get; set; }
        
        [Required]
        public string Period { get; set; } = "monthly"; // "monthly" or "yearly"
        
        public decimal Spent { get; set; } = 0;
        
        public Category? Category { get; set; }
    }
}