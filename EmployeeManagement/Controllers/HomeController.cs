using System.Collections.Generic;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Controllers
{
    //[Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger logger;

        public HomeController(IEmployeeRepository employeeRepository,
            IWebHostEnvironment webHostEnvironment,
            ILogger logger)
        {
            this._employeeRepository = employeeRepository;
            this._webHostEnvironment = webHostEnvironment;
            this.logger = logger;
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
            ////界面传参的三种方式
            ////1、ViewData 自定义类型需要在 view 中进行 as 转换
            //Employee? model2 = _employeeRepository.GetEmployee(2);
            //ViewData["Employee2"] = model2;
            //ViewData["Title2"] = model2?.Name ?? "空2";
            ////2、ViewBag 自定义类型无智能提示，不需要进行 as 转换
            //Employee? model3 = _employeeRepository.GetEmployee(3);
            //ViewBag.Employee3 = model3;
            //ViewBag.Title3 = model3?.Name ?? "空3";
            //3、Strongly typed 
            //Employee? model1 = _employeeRepository.GetEmployee(id);            
            ////return View(model1);
            ///
            logger.LogTrace("LogTrace");
            logger.LogDebug("LogDebug");
            logger.LogInformation("LogInformation");
            logger.LogWarning("LogWarning");
            logger.LogError("LogError");
            logger.LogCritical("LogCritical");
            throw new Exception("Error in home/details");

            Employee? model1 = _employeeRepository.GetEmployee(id);
            if (model1 == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound",id);
            }

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
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (!ModelState.IsValid) return View();
            string uniqueFileName = ProcessUploadFile(model);
            Employee newEmployee = new Employee()
            {
                Name = model.Name,
                Email = model.Email,
                Department = model.Department,
                PhotoUrl = uniqueFileName
            };

            _employeeRepository.Add(newEmployee);
            return RedirectToAction("details", new { id = newEmployee.Id });
        }

        private string ProcessUploadFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                model.Photo.CopyTo(fileStream);
            }

            return uniqueFileName;
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoUrl = employee.PhotoUrl,
            };
            return View(employeeEditViewModel);
        }

        [HttpPost]
        public IActionResult Update(EmployeeEditViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("edit", new { id = model.Id });
            Employee newEmployee = _employeeRepository.GetEmployee(model.Id);
            newEmployee.Id = model.Id;
            newEmployee.Name = model.Name;
            newEmployee.Email = model.Email;
            newEmployee.Department = model.Department;
            if (model.Photo != null)
            {
                if (model.ExistingPhotoUrl != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", model.ExistingPhotoUrl);
                    System.IO.File.Delete(filePath);
                }
                newEmployee.PhotoUrl = ProcessUploadFile(model);
            }
            _employeeRepository.Update(newEmployee);
            return RedirectToAction("index");

        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            if (employee.PhotoUrl != null)
            {
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", employee.PhotoUrl);
                System.IO.File.Delete(filePath);
            }
            if (employee == null) return RedirectToAction("index");
            _employeeRepository.Delete(employee.Id);
            return RedirectToAction("index");
        }

    }
}
