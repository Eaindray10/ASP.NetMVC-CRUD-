using CRUD_MVC_.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_MVC_.Controllers
{
    public class EmployeeController : Controller
    {
        string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
       

        public List<Employee> empList = new List<Employee>();
        // GET: Employee
        public ActionResult Emp_View()
        {
            var emps = from e in getEmpList() orderby e.emp_id select e;
            return View(emps);
        }

        public ActionResult Details(int id)
        {
            List<Employee> emps = getEmpList();
            var emp = emps.Single(e => e.emp_id == id);
            return View(emp);
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
                MySqlConnection con = new MySqlConnection(constr);
                con.Open();
                string insert_query = "insert into employee(emp_id,emp_name,age) " +
                    "values(" + Int32.Parse(id)+", '" +
                    collection["emp_name"] + "', " +
                    Int32.Parse(age)+
                    ");";
                MySqlCommand cmd = new MySqlCommand(insert_query, con);
                cmd.ExecuteNonQuery();
                con.Close();
                //empList.Add(emp);
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
                MySqlConnection con = new MySqlConnection(constr);
                con.Open();
                string update_query = "update employee set " +
                    "emp_name='" + collection["emp_name"] + "'," +
                    "age=" + Convert.ToInt32(collection["age"]) + " where emp_id="+id+";";
                MySqlCommand cmd = new MySqlCommand(update_query, con);
                cmd.ExecuteNonQuery();
                con.Close();
                return RedirectToAction("Emp_View");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                MySqlConnection con = new MySqlConnection(constr);
                con.Open();
                string delete_query = "delete from employee where emp_id=" + id + ";";
                MySqlCommand cmd = new MySqlCommand(delete_query, con);
                cmd.ExecuteNonQuery();
                con.Close();
                //var emp = empList.Single(e => e.emp_id == id);
                //empList.Remove(emp);
                //if (TryUpdateModel(empList))
                //    return RedirectToAction("Emp_View");
                return RedirectToAction("Emp_View");
            }
            catch
            {
                return View();
            }
        }

        public List<Employee> getEmpList()
        {
            MySqlConnection con = new MySqlConnection(constr);
            con.Open();
            string select_query = "select emp_id,emp_name,age from employee;";
            MySqlCommand cmd = new MySqlCommand(select_query, con);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                empList.Add(
                    new Employee { emp_id = Convert.ToInt32(reader.GetValue(0).ToString()), emp_name = reader.GetValue(1).ToString(), age = Convert.ToInt32(reader.GetValue(2).ToString()) }
                    );
            }
            con.Close();
            return empList;
        }
    }
}