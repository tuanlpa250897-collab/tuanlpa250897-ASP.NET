using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string Password { get; set; } = string.Empty;
        
        public string Role { get; set; } = "user";
        
        public string? Avatar { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}