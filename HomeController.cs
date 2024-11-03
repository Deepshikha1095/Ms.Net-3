using DalLib;
using Ninject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppDal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDal dal;
        public HomeController()
        {
            IKernel kernel = new StandardKernel();
            string repoType = ConfigurationManager.AppSettings["repoName"];
            Type type = Type.GetType($"DalLib.{repoType},DalLib");
            if (repoType == "ActualDal")
            {
                string cnnStr = ConfigurationManager.ConnectionStrings["ConnectDB"].ConnectionString;
                kernel.Bind<IDal>().To(type).WithConstructorArgument(cnnStr);   

            }
            else kernel.Bind<IDal>().To(type);
            dal=kernel.Get<IDal>(); 

        }
        public ActionResult Index()
        {
            List<Employee> employees= dal.GetAllEmployee();
            return View(employees);
        }

        public ActionResult AddEmployee()
        {
            
            return View(new Employee());
        }

        [HttpPost]
        public ActionResult AddEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                bool isAdded = dal.AddEmployee(employee);
                if (isAdded) 
                return RedirectToAction("Index");
                ModelState.AddModelError("", "Error adding employee");
            }
            return View(employee);
        }
        public ActionResult EditEmployee(int id)
        {
        var emp= dal.GetEmployeeById(id);
            return View(emp);

        
        }
        [HttpPost]
        public ActionResult EditEmployee(Employee emp)
        {
            var isEdited = dal.ModifyEmployee(emp);
            if (ModelState.IsValid)
            {
          
                if (isEdited)
                    return RedirectToAction("Index");
                ModelState.AddModelError("", "Error editing  employee");
            }
            return View(emp);


        }
        public ActionResult DeleteEmployee(int id)
        {
            var isDeleted = dal.RemoveEmployee(id);
            if (ModelState.IsValid)
            {
                
                if (isDeleted)
                    return RedirectToAction("Index");
                ModelState.AddModelError("", "Error Deleting employee");
            }
            return View();
        }

    }
}