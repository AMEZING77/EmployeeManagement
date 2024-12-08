using Microsoft.AspNetCore.Components;

namespace EmployeeManagement.Controllers
{
    //[Route("[controller]/[action]")]
    public class DepartmentController
    {
        public string List()
        {
            return "List() of DepartmentController";
        }

        public string Details()
        {
            return "Details() of DepartmentController";
        }
    }
}
