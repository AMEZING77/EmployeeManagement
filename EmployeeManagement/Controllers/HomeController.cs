using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }
        public JsonResult Index()
        {
            return this.Json(_employeeRepository.GetEmployee(1));
            //return this.Json(new { id = 1, name = "zj" });
        }
        public string Details_String()
        {
            return _employeeRepository.GetEmployee(1)?.Name ?? "空";
        }
        public JsonResult Details_Json()
        {
            return this.Json(_employeeRepository.GetEmployee(1)?.Name ?? "空");
        }

        public ObjectResult Details_Xml()
        {
            return new ObjectResult(_employeeRepository.GetEmployee(1));
        }

        public ViewResult Details_View()
        {
            return View(_employeeRepository.GetEmployee(1));
        }

    }
}
