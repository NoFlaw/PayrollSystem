using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PayrollSystemDemo.Data.Models;
using PayrollSystemDemo.Service.Base;
using PayrollSystemDemo.Service.Helpers;

namespace PayrollSystemDemo.Web.Controllers
{
    public class DependentsController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IBenefitCostService _benefitCostService;
        private readonly IDiscountService _discountService;
        private readonly IDependentService _dependentService;

        public DependentsController
            (
            IDependentService dependentService, 
            IDiscountService discountService, 
            IBenefitCostService benefitCostService, 
            IEmployeeService employeeService
            )
        {
            _dependentService = dependentService;
            _discountService = discountService;
            _benefitCostService = benefitCostService;
            _employeeService = employeeService;
        }

        // GET: Dependents
        public ActionResult Index()
        {
            var dependents = _dependentService.GetAll().ToList();
            return View(dependents);
        }

        // GET: Dependents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
       
            var dependent = _dependentService.GetDependentById(Convert.ToInt32(id));
            return dependent == null ? (ActionResult) HttpNotFound() : View(dependent);
        }

        // GET: Dependents/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(_employeeService.GetAll(), "EmployeeId", "FullName");
            ViewBag.DiscountId = new SelectList(_discountService.GetAll(), "DiscountId", "DiscountTitle");
            ViewBag.DependentTypeId = new SelectList(_dependentService.GetAllDependentTypes(), "DependentTypeId", "DependentTypeName");
          
            return View();
        }

        // GET: Dependents/Add/5
        public ActionResult Add(int? id)
        {
            if (id <= 0 || id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
       
            var employee = _employeeService.GetById(Convert.ToInt32(id));

            if (employee == null)
                return HttpNotFound();
  
            ViewBag.EmpId = employee.EmployeeId;
            ViewBag.DiscountId = new SelectList(_discountService.GetAll(), "DiscountId", "DiscountTitle");
            ViewBag.EmployeeId = new SelectList(_employeeService.GetAll(), "EmployeeId", "FullName", employee.EmployeeId); 
            ViewBag.DependentTypeId = new SelectList(_dependentService.GetAllDependentTypes(), "DependentTypeId", "DependentTypeName");
            
            return View();
        }

        // POST: Dependents/Add
        [HttpPost]
        public ActionResult Add(Dependent dependent)
        {
            if (ModelState.IsValid)
            {
                dependent.DateCreated = DateTime.Now;
                dependent.DiscountId = DiscountHelper.GetDiscountByName(dependent.FirstName, dependent.LastName);
                dependent.BenefitCost = _benefitCostService.GetDependentBenefitCost();
                var savedDependent = _dependentService.Create(dependent);
                
                return RedirectToAction("Edit", "Employees", new { id = dependent.EmployeeId });
            }
           
            ViewBag.EmployeeId = new SelectList(_employeeService.GetAll(), "EmployeeId", "FullName", dependent.EmployeeId);
            ViewBag.DiscountId = new SelectList(_discountService.GetAll(), "DiscountId", "DiscountTitle", dependent.DiscountId);
            ViewBag.DependentTypeId = new SelectList(_dependentService.GetAllDependentTypes(), "DependentTypeId", "DependentTypeName", dependent.DependentTypeId);
            
            return View(dependent);
        }

        // POST: Dependents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DependentId,FirstName,LastName,EmployeeId,DependentTypeId,DiscountId,DateCreated")] Dependent dependent)
        {
            if (ModelState.IsValid)
            {
                dependent.DateCreated = DateTime.Now;
                dependent.DiscountId = DiscountHelper.GetDiscountByName(dependent.FirstName, dependent.LastName);
                dependent.BenefitCost = _benefitCostService.GetDependentBenefitCost();
                var savedDependent = _dependentService.Create(dependent);
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(_employeeService.GetAll(), "EmployeeId", "FirstName", dependent.EmployeeId);
            ViewBag.DiscountId = new SelectList(_discountService.GetAll(), "DiscountId", "DiscountTitle", dependent.DiscountId);
            ViewBag.DependentTypeId = new SelectList(_dependentService.GetAllDependentTypes(), "DependentTypeId", "DependentTypeName", dependent.DependentTypeId);

            return View(dependent);
        }

        // GET: Dependents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        
            var dependent = _dependentService.GetDependentById(Convert.ToInt32(id));
            
            if (dependent == null)
                return HttpNotFound();
     
            ViewBag.EmployeeId = new SelectList(_employeeService.GetAll(), "EmployeeId", "FullName", dependent.EmployeeId);
            ViewBag.DiscountId = new SelectList(_discountService.GetAll(), "DiscountId", "DiscountTitle", dependent.DiscountId);
            ViewBag.DependentTypeId = new SelectList(_dependentService.GetAllDependentTypes(), "DependentTypeId", "DependentTypeName", dependent.DependentTypeId);
            
            return View(dependent);
        }

        // POST: Dependents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DependentId,FirstName,LastName,EmployeeId,DependentTypeId,DiscountId,DateCreated")] Dependent dependent)
        {
            if (ModelState.IsValid)
            {
                dependent.DiscountId = DiscountHelper.GetDiscountByName(dependent.FirstName, dependent.LastName);
                var updatedDependent = _dependentService.Update(dependent);
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(_employeeService.GetAll(), "EmployeeId", "FirstName", dependent.EmployeeId);
            ViewBag.DiscountId = new SelectList(_discountService.GetAll(), "DiscountId", "DiscountTitle", dependent.DiscountId);
            ViewBag.DependentTypeId = new SelectList(_dependentService.GetAllDependentTypes(), "DependentTypeId", "DependentTypeName", dependent.DependentTypeId);
          
            return View(dependent);
        }

        // GET: Dependents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            var dependent = _dependentService.GetDependentById(Convert.ToInt32(id));

            return dependent == null ? (ActionResult) HttpNotFound() : View(dependent);
        }

        // POST: Dependents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var dependent = _dependentService.GetDependentById(Convert.ToInt32(id));
            _dependentService.Delete(dependent);
            return RedirectToAction("Index");
        }
    }
}
