﻿
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Models
{
    public class SQLEmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext context;

        public SQLEmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Employee Add(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
            return employee;
        }

        public Employee Delete(int id)
        {
            var employee = context.Employees.Find(id);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
            return employee;
        }

        public IEnumerable<Employee> GetAll()
        {
            return context.Employees;
        }

        public Employee? GetEmployee(int id)
        {
            return context.Employees.Find(id);
        }

        public Employee Update(Employee employeeChanges)
        {
            context.Entry(employeeChanges).State = EntityState.Modified;
            //var employee = context.Employees.Attach(employeeChanges);
            //employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return employeeChanges;
        }
    }
}
