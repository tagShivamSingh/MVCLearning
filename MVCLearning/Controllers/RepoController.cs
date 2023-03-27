using MVCLearning.Domain;
using MVCLearning.Models;
using MVCTutorial.Business;
using MVCTutorial.Business.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeDomainModel = MVCLearning.Domain.EmployeeDomainModel;

namespace MVCLearning.Controllers
{
    public class RepoController : Controller
    {
        IEmployeeBusiness _empBusiness;
        //oop principle:depend on the abstraction not on the concrete classes 
        // GET: Repo
        public RepoController(IEmployeeBusiness empBusiness)
        {
            _empBusiness = empBusiness;
        }
        public ActionResult Index()
        {
            //IEmployeeBusiness empBusiness = new EmployeeBusiness();
            ViewBag.EmpName = _empBusiness.GetEmployeeName(254);
            //ViewBag.EmployeeList = empBusiness.GetAllEmployee();
            List<EmployeeDomainModel> listDomain = _empBusiness.GetAllEmployee();
            List<EmployeeViewModel> listEmployeeVM = new List<EmployeeViewModel>();
            AutoMapper.Mapper.Map(listDomain, listEmployeeVM);
            ViewBag.EmployeeList = listEmployeeVM;
            return View();
        }
    }
}