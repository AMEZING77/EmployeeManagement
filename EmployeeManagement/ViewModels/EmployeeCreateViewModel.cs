using EmployeeManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModels
{
    public class EmployeeCreateViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 chars")]
        public required string Name { get; set; }
        [Required]
        [Display(Name = "Office Email")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
            ErrorMessage = "Invalid Format")]
        public required string Email { get; set; }
        //[Required]//对于值类型来说，有默认的值，因此可以不需要Required
        public Dept? Department { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
