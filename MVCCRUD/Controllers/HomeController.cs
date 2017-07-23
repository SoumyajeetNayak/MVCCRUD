using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCCRUD.Models;

namespace MVCCRUD.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetEmployees()
        {
          using (CRUDEntities dc = new CRUDEntities()) 
          {
            var employee = dc.Empployees.OrderBy(a => a.FirstName).ToList();
            return Json(new { data = employee }, JsonRequestBehavior.AllowGet);
          }
        }

        [HttpGet]
        public  ActionResult Save(int id)
        {
          using(CRUDEntities dc = new CRUDEntities())
          {
            var v = dc.Empployees.Where(a => a.EmployeeID == id).FirstOrDefault();
            return View(v);
          }
        }

        [HttpPost]
        public ActionResult Save(MVCCRUD.Models.Empployee emp)
        {
          bool status = false;
          if(ModelState.IsValid)
          {
            using(CRUDEntities dc = new CRUDEntities())
            {
              if(emp.EmployeeID > 0)
              {
                //Edit
                var v = dc.Empployees.Where(a => a.EmployeeID == emp.EmployeeID).FirstOrDefault();
                if (v != null) 
                {
                  v.FirstName = emp.FirstName;
                  v.LastName = emp.LastName;
                  v.EmailID = emp.EmailID;
                  v.City = emp.City;
                  v.Country = emp.Country;
                }

              }
              else
              {
                //save
                dc.Empployees.Add(emp);
              }

              dc.SaveChanges();
              status = true;
            }
          }
          return new JsonResult { Data = new { status = status } };
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
          using(CRUDEntities dc = new CRUDEntities())
          {
            var v = dc.Empployees.Where(a => a.EmployeeID == id).FirstOrDefault();
            if(v != null)
            {
              return View(v);
            }
            else
            {
              return HttpNotFound();
            }
          }
        }
        
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteEmployee(int id)
        {
          bool status = false;
          using( CRUDEntities dc = new CRUDEntities())
          {
            var v = dc.Empployees.Where(a=>a.EmployeeID == id).FirstOrDefault();
            if(v != null)
            {
              dc.Empployees.Remove(v);
              dc.SaveChanges();
              status = true;
            }

          }

          return new JsonResult{ Data = new {status = status}};
        }
	}
}