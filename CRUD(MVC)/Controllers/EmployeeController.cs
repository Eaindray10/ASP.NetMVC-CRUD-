using CRUD_MVC_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_MVC_.Controllers
{
    public class EmployeeController : Controller
    {
        public static List<Employee> empList = new List<Employee>
        {
             new Employee{emp_id=1,emp_name="Eaindray",age=23},
                new Employee{emp_id=2,emp_name="Htet",age=24},
                 new Employee{emp_id=3,emp_name="Eaindray",age=23},
                new Employee{emp_id=4,emp_name="Htet",age=24}
        };
        // GET: Employee
        public ActionResult Emp_View()
        {
            var emps = from e in empList orderby e.emp_id select e;
            return View(emps);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Employee emp = new Employee();
                string id = collection["emp_id"];
                emp.emp_id = Int32.Parse(id);
                emp.emp_name = collection["emp_name"];
                string age = collection["age"];
                emp.age = Int32.Parse(age);
                empList.Add(emp);
                return RedirectToAction("Emp_View");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            List<Employee> emplist = getEmpList();
            var emp = emplist.Single(e => e.emp_id == id);
            return View(emp);
        }

        [HttpPost]
        public ActionResult Edit(int id,FormCollection collection)
        {
            try
            {
                var emp = empList.Single(e => e.emp_id == id);
                if (TryUpdateModel(emp))
                    return RedirectToAction("Emp_View");
                return View(emp);
            }
            catch
            {
                return View();
            }
        }

        public List<Employee> getEmpList()
        {
            return empList;
        }
    }
}