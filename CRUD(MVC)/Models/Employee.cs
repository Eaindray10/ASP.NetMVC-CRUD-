using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CRUD_MVC_.Models
{
    public class Employee
    {
        public int emp_id { get; set; }
        public string emp_name{get; set;}
        public int age { get; set; }
    }
}