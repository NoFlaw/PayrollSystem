using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PayrollSystemDemo.Data.Models;
using PayrollSystemDemo.Service.Base;
using PayrollSystemDemo.Service.Helpers;
using PayrollSystemDemo.Web.Models;

namespace PayrollSystemDemo.Web.Controllers
{
    //Todo: Clean this up first - http://stackoverflow.com/questions/10039006/mvc-dropdownlist-selectedvalue-not-displaying-correctly
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IBenefitCostService _benefitCostService;
        private readonly ISalaryService _salaryService;
        private readonly IDiscountService _discountService;
        private readonly IDependentService _dependentService;

        public EmployeesController
            (
            IEmployeeService employeeService, 
            IBenefitCostService benefitCostService,
            ISalaryService salaryService, 
            IDiscountService discountService, 
            IDependentService dependentService
            )
        {
            _employeeService = employeeService;
            _benefitCostService = benefitCostService;
            _salaryService = salaryService;
            _discountService = discountService;
            _dependentService = dependentService;
        }

        // GET: Employees
        public ActionResult Index()
        {
            var employees = _employeeService.GetAll().ToList();
            return View(employees);
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            
            var employee = _employeeService.GetById(Convert.ToInt32(id));

            return employee == null ? (ActionResult) HttpNotFound() : View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.SalaryId = new SelectList(_salaryService.GetAll(), "SalaryId", "SalaryFormated");
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,FirstName,LastName,SalaryId,DiscountId,DateCreated,DependentTypeId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.DateCreated = DateTime.Now;

                employee.DiscountId = DiscountHelper.GetDiscountByName(employee.FirstName, employee.LastName);

                employee.BenefitCost = _benefitCostService.GetEmployeeBenefitCost();

                var savedEmployee = _employeeService.Create(employee);

                return RedirectToAction("Index");
            }

            ViewBag.SalaryId = new SelectList(_salaryService.GetAll(), "SalaryId", "SalaryFormated", employee.SalaryId);

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var employee = _employeeService.GetById(Convert.ToInt32(id));
              
            if (employee == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.DiscountId = new SelectList(_discountService.GetAll(), "DiscountId", "DiscountTitle", employee.DiscountId);
            ViewBag.SalaryId = new SelectList(_salaryService.GetAll(), "SalaryId", "SalaryFormated", employee.SalaryId);
            ViewBag.DependentTypeId = new SelectList(_dependentService.GetAllDependentTypes(), "DependentTypeId", "DependentTypeId", employee.DependentTypeId);

            var viewModel = new EmployeeDependentViewModel
            {
                Employee = employee,
                Dependents = employee.Dependents    
            };

            return View(viewModel);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,FirstName,LastName,SalaryId,DiscountId,BenefitCostId,DateCreated,DependentTypeId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.DiscountId = DiscountHelper.GetDiscountByName(employee.FirstName, employee.LastName);
                employee = _employeeService.Update(employee);
                return RedirectToAction("Edit", new { id = employee.EmployeeId });
            }

            ViewBag.DiscountId = new SelectList(_discountService.GetAll(), "DiscountId", "DiscountTitle", employee.DiscountId);
            ViewBag.SalaryId = new SelectList(_salaryService.GetAll(), "SalaryId", "SalaryRate", employee.SalaryId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
           
            var employee = _employeeService.GetById(Convert.ToInt32(id));
            return employee == null ? (ActionResult) HttpNotFound() : View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var employee = _employeeService.GetById(Convert.ToInt32(id));
            _employeeService.Delete(employee);
            return RedirectToAction("Index");
        }

    }
}
