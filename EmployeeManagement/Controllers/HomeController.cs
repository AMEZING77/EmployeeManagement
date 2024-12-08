using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
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
        public ViewResult Index()
        {
            //return this.Json(_employeeRepository.GetEmployee(1));
            //return this.Json(new { id = 1, name = "zj" });
            var list = _employeeRepository.GetAll();
            return View(list);
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
            //界面传参的三种方式
            //1、ViewData 自定义类型需要在 view 中进行 as 转换
            Employee? model2 = _employeeRepository.GetEmployee(2);
            ViewData["Employee2"] = model2;
            ViewData["Title2"] = model2?.Name ?? "空2";
            //2、ViewBag 自定义类型无智能提示，不需要进行 as 转换
            Employee? model3 = _employeeRepository.GetEmployee(3);
            ViewBag.Employee3 = model3;
            ViewBag.Title3 = model3?.Name ?? "空3";
            //3、Strongly typed 
            Employee? model1 = _employeeRepository.GetEmployee(1);
            //return View(model1);

            //4、ViewModel
            HomeDetails_ViewViewModel homeDetails = new HomeDetails_ViewViewModel()
            {
                Employee = model1,
                PageTitle = "Title From ViewModel",
            };

            return View(homeDetails);


        }

    }
}
