using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
               new Employee
               {
                   Id = 1,
                   Name = "InitName",
                   Department = Dept.IT,
                   Email = "Init@email.com",
               },
                new Employee
                {
                    Id = 2,
                    Name = "ChangeName",
                    Department = Dept.PayRoll,
                    Email = "Change@email.com",
                }
               );
        }
    }
}
