﻿using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    //[Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public HomeController(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        //默认定义此入口，可以减少 action 的引用
        //[Route("~/Home")]
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
        //[Route("{id?}")]
        public ViewResult Details(int id = 1)
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
            Employee? model1 = _employeeRepository.GetEmployee(id);
            //return View(model1);

            //4、ViewModel
            HomeDetailsViewModel homeDetails = new HomeDetailsViewModel()
            {
                Employee = model1,
                PageTitle = "Title From ViewModel",
            };

            //return View(homeDetails);
            return View(homeDetails);

        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (!ModelState.IsValid) return View();
            Employee newEmployee = _employeeRepository.Add(employee);
            return RedirectToAction("details", new { id = newEmployee.Id });
        }

    }
}
