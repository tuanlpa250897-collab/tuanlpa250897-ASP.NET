using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models
{
    public class Transaction
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
        public string Description { get; set; } = string.Empty;
        
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
        
        [Required]
        public string Type { get; set; } = string.Empty; // "income" or "expense"
        
        public Category? Category { get; set; }
    }
}