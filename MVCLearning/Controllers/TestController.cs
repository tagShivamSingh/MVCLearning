using MVCLearning.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Generator;
using System.Web.Routing;
using System.Web.UI.WebControls.WebParts;
namespace MVCLearning.Controllers
{
    //[RouteArea("City")]
    //[RoutePrefix("Home")]
    //[Route("{action=index}")]
    public class TestController : Controller
    {
        // GET: Test
        
        public ActionResult Index()
        {
            MVCLearningEntities db = new MVCLearningEntities();

            List<department> list = db.departments.ToList();
            ViewBag.departmentList = new SelectList(list, "DId", "dname");

            


            //List<ModelA> list_A = new List<ModelA>();
            //List<ModelB> list_B = new List<ModelB>();

            //list_A.Add(new ModelA { Name = "Rishab" });
            //list_A.Add(new ModelA { Name = "Rohit" });
            //list_A.Add(new ModelA { Name = "Ritesh" });


            //list_B.Add(new ModelB { Country = "India" });
            //list_B.Add(new ModelB { Country = "US" });

            //list_B.Add(new ModelB { Country = "UK" });

            //ModelC finalItem = new ModelC();
            //finalItem.ListA = list_A;
            //finalItem.ListB = list_B;
            //finalItem.Age = 12;


            //return View(finalItem);
            //ViewBag.CountryList = new SelectList(GetCountryList(), "CountryId", "CountryName");

            //MVCLearningEntities db = new MVCLearningEntities();

            //List<EmployeeViewModel> list = db.Employees.Select(x => new EmployeeViewModel { Name = x.Name, EmployeeId = x.EmployeeId, dname = x.department.dname, Address = x.Address }).ToList();
            //ViewBag.EmployeeList = list;

            //List<MyShop> ItemList = new List<MyShop>();
            //ItemList.Add(new MyShop { ItemID = 1, ItemName = "Rice", IsAvailable = true });
            //ItemList.Add(new MyShop { ItemID = 2, ItemName = "Pulse", IsAvailable = false });
            //ItemList.Add(new MyShop { ItemID = 3, ItemName = "Salt", IsAvailable = false });
            //ItemList.Add(new MyShop { ItemID = 4, ItemName = "Sugar", IsAvailable = true });
            //ItemList.Add(new MyShop { ItemID = 5, ItemName = "Soap", IsAvailable = false});
            //ItemList.Add(new MyShop { ItemID = 6, ItemName = "Book", IsAvailable = true });
            //ViewBag.ItemList = ItemList;

            return View();
        }
        public JsonResult DeleteEmployee(int EmployeeId)
        {
            MVCLearningEntities db = new MVCLearningEntities();
            bool result = false;
            Employee emp = db.Employees.SingleOrDefault(x => x.IsDelete == false && x.EmployeeId == EmployeeId);
            if (emp != null)
            {
                emp.IsDelete = true;
                db.SaveChanges();
                result = true;
            }
            return Json(result,JsonRequestBehavior.AllowGet);
        }
        public ActionResult ShowEmployee(int EmployeeId)
        {
            MVCLearningEntities db = new MVCLearningEntities();
            List<EmployeeViewModel> listEmp = db.Employees.Where(x => x.IsDelete == false && x.EmployeeId==EmployeeId).Select(x => new EmployeeViewModel { Name = x.Name, dname = x.department.dname, Address = x.Address, EmployeeId = x.EmployeeId }).ToList();
            ViewBag.EmployeeList = listEmp;

            return PartialView("Partial1");
        }

        public ActionResult AddEditEmployee(int EmployeeId)
        {
            MVCLearningEntities db = new MVCLearningEntities();
            List<department> list = db.departments.ToList();
            ViewBag.departmentList = new SelectList(list, "DId", "dname");

            EmployeeViewModel model = new EmployeeViewModel();
            if (EmployeeId > 0)
            {
                Employee emp = db.Employees.SingleOrDefault(x => x.IsDelete == false && x.EmployeeId == EmployeeId);
                model.EmployeeId = emp.EmployeeId;
                model.DId = emp.DId;
                model.Name = emp.Name;
                model.Address = emp.Address;
            }


            return PartialView("Partial2",model);
        }
        [HttpPost]
        public ActionResult Index(EmployeeViewModel model)
        {
            try
            {
                MVCLearningEntities db = new MVCLearningEntities();
                List<department> list = db.departments.ToList();
                ViewBag.departmentList = new SelectList(list, "DId", "dname");

                if (model.EmployeeId > 0)
                {
                    //Update
                    Employee emp = db.Employees.SingleOrDefault(x => x.IsDelete == false && x.EmployeeId == model.EmployeeId);
                    emp.DId=model.DId;
                    emp.Name=model.Name ;
                    emp.Address=model.Address;
                    db.SaveChanges();
                }
                else
                {
                    //Insert
                    Employee emp = new Employee();
                    emp.Name = model.Name;
                    emp.Address = model.Address;
                    emp.DId = model.DId;
                    emp.IsDelete = false;
                    db.Employees.Add(emp);
                    db.SaveChanges();
                }
                return View(model);


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        [HttpPost]

        public ActionResult Index2(EmployeeViewModel model)
        {
            if (ModelState.IsValid == true)
            {
                //Write Your Code Here
            }
            MVCLearningEntities db = new MVCLearningEntities();
            List<department> list = db.departments.ToList();
            ViewBag.departmentList = new SelectList(list, "DId", "dname");
            return View(model);
        }
        public ActionResult EmployeeDetail(int EmployeeId)
        {
            MVCLearningEntities db = new MVCLearningEntities();
            Employee employee = db.Employees.SingleOrDefault(x=>x.EmployeeId==EmployeeId);
            EmployeeViewModel employeeVM = new EmployeeViewModel();

            employeeVM.Name = employee.Name;
            employeeVM.Address = employee.Address;
            employeeVM.dname = employee.department.dname;


            return View(employeeVM);
        }

        public ActionResult SaveRecord(EmployeeViewModel model)
        {
            try
            {
                MVCLearningEntities db = new MVCLearningEntities();
                Employee emp = new Employee();
                emp.Name = model.Name;
                emp.Address = model.Address;
                emp.DId = model.DId;
                db.Employees.Add(emp);
                db.SaveChanges();
                int latestid = emp.EmployeeId;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public JsonResult RegisterUser(RegistrationViewModel model)
        {
            MVCLearningEntities db = new MVCLearningEntities();


            SiteUser siteUser = new SiteUser();

            siteUser.UserName = model.UserName;

            siteUser.EmailId = model.EmailId;

            siteUser.Password = model.Password;

            siteUser.Address = model.Address;

            siteUser.RoleId = 2;

            db.SiteUsers.Add(siteUser);

            db.SaveChanges();


            return Json("Success", JsonRequestBehavior.AllowGet);

        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public JsonResult LoginUser(LoginViewModel model)
        {
            MVCLearningEntities db = new MVCLearningEntities();
            SiteUser user = db.SiteUsers.SingleOrDefault(x=>x.EmailId==model.EmailId && x.Password==model.Password);
            string result = "fail";
            if(user!=null)
            {
                Session["UserId"] = user.UserId;
                Session["UserName"] = user.UserName;

                if(user.RoleId==2)
                {
                    result = "General User";
                }
                else if(user.RoleId==1)
                {
                    result = "Admin";
                }
                else
                {

                }
            }
            return Json("",JsonRequestBehavior.AllowGet);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login");
        }
    
        public ActionResult SideMenu()
        {
            List<MenuItem> list = new List<MenuItem>();
            list.Add(new MenuItem { Link = "/Test/Index", LinkName = "Index" });
            list.Add(new MenuItem { Link = "/Test/Login", LinkName = "Login" });
            list.Add(new MenuItem { Link = "/Test/Registration", LinkName = "Register" });

            return PartialView("SideMenu",list);
        }
        public JsonResult ImageUpload(ProductViewModel model)
        {
            MVCLearningEntities db = new MVCLearningEntities();
            int imageID = 0;
            var file = model.ImageFile;
            byte[] imagebyte = null;
            if(file!=null)
            {
                
                file.SaveAs(Server.MapPath("/Scripts/UploadedImage/" + file.FileName));
                BinaryReader reader = new BinaryReader(file.InputStream);
                imagebyte = reader.ReadBytes(file.ContentLength);
                ImageStore img = new ImageStore();
                img.ImageName = file.FileName;
                img.ImageByte = imagebyte;
                img.ImagePath = "/Scripts/uploadedImage/" + file.FileName;
                img.IsDeleted = false;
                db.ImageStores.Add(img);
                db.SaveChanges();

                imageID = img.ImageId;

                
            }
            if(model.ImageUrl!=null)
            {
                imagebyte = DownloadImage(model.ImageUrl);
                ImageStore img = new ImageStore();
                img.ImageName = "Abc";
                img.ImageByte = imagebyte;
                img.ImagePath =model.ImageUrl;
                img.IsDeleted = false;
                db.ImageStores.Add(img);
                db.SaveChanges();

                imageID = img.ImageId;
            }
            return Json(imageID, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ImageRetrieve(int imgId)
        {
            MVCLearningEntities db = new MVCLearningEntities();
            var img = db.ImageStores.SingleOrDefault(x => x.ImageId == imgId);
            return File(img.ImageByte, "image/jpg");

        }
        public byte[] DownloadImage(string Url)
        {
            var webclient = new WebClient();
            byte[] imagebytes = webclient.DownloadData(Url);
            return imagebytes;

        }
        public List<Country> GetCountryList()
        {
            MVCLearningEntities db = new MVCLearningEntities();

            List<Country> countries = db.Countries.ToList();
            return countries;
        }
        public ActionResult GetStateList(int CountryId)
        {
            MVCLearningEntities db = new MVCLearningEntities();
            List<State> stateList = db.States.Where(x=>x.CountryId==CountryId).ToList();
            ViewBag.StateOptions = new SelectList(stateList, "StateId", "StateName");
            return PartialView("StateOptionPartial");
        }
        public ActionResult GetSearchRecord(string SearchText)
        {
            MVCLearningEntities db = new MVCLearningEntities();
            List<EmployeeViewModel> list = db.Employees.Where(x=>x.Name.Contains(SearchText)||x.department.dname.Contains(SearchText)).Select(x => new EmployeeViewModel { Name = x.Name, EmployeeId = x.EmployeeId, dname = x.department.dname, Address = x.Address }).ToList();

            return PartialView("SearchPartial", list);
        }
        [Route("Student")]
        public string GetStudent()
        {
            return "All Student Record";
        }
        [Route("Student/{Id:int:min(2):max(10)}")]
        public string GetStudent(int Id)
        {
            return "Student with ID: " + Id;
        }
        [Route("Student/{Name}")]
        public string GetStudent(string Name)
        {
            return "Student with Name: " + Name;
        }
        public JsonResult SaveList(string ItemList)
        {
            string[] arr = ItemList.Split(',');
            foreach(var id in arr)
            {
                var currentid = id;
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSuggestion(string text)
        {
            List<MyShop> ItemList = new List<MyShop>();
            ItemList.Add(new MyShop { ItemID = 1, ItemName = "Rice", IsAvailable = true });
            ItemList.Add(new MyShop { ItemID = 2, ItemName = "Pulse", IsAvailable = false });
            ItemList.Add(new MyShop { ItemID = 3, ItemName = "Salt", IsAvailable = false });
            ItemList.Add(new MyShop { ItemID = 4, ItemName = "Sugar", IsAvailable = true });
            ItemList.Add(new MyShop { ItemID = 5, ItemName = "Soap", IsAvailable = false });
            ItemList.Add(new MyShop { ItemID = 6, ItemName = "Book", IsAvailable = true });
            ItemList.Add(new MyShop { ItemID = 1, ItemName = "Apple", IsAvailable = true });
            ItemList.Add(new MyShop { ItemID = 2, ItemName = "Aeroplane", IsAvailable = false });
            ItemList.Add(new MyShop { ItemID = 1, ItemName = "Orange", IsAvailable = false });
            ItemList.Add(new MyShop { ItemID = 2, ItemName = "Boy", IsAvailable = true });
            ItemList.Add(new MyShop { ItemID = 1, ItemName = "Blackberry", IsAvailable = false });
            ItemList.Add(new MyShop { ItemID = 2, ItemName = "Levis", IsAvailable = true });
            ItemList.Add(new MyShop { ItemID = 1, ItemName = "Women", IsAvailable = false });
            ItemList.Add(new MyShop { ItemID = 2, ItemName = "C++", IsAvailable = true });

            List<string> list = new List<string>();
            list = ItemList.Where(x => x.ItemName.ToLower().Contains(text.ToLower())).Select(x => x.ItemName).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SendMailToUser()
        {
            bool result = false;
            result = SendEmail("sharmarishab777@gmail.com", "For Sample Email", "<p>Hi Rishab, <br /> This email is for testing purpose</p>");
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public bool SendEmail(string toEmail,string subject,string emailbody)
        {
            try
            {
                string senderEmail = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"].ToString();
                string senderPassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"].ToString();

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, senderPassword);

                MailMessage mailMessage = new MailMessage(senderEmail, toEmail, subject, emailbody);
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                client.Send(mailMessage);

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        public JsonResult GetEmployeeRecord(DataTablesParam param, string EName)
        {
            MVCLearningEntities db = new MVCLearningEntities();
            //List<EmployeeViewModel> listEmp = new List<EmployeeViewModel>();
            List<EmployeeDomainModelWeb> List = new List<EmployeeDomainModelWeb>();

            List<EmployeeViewModel> empVMList = new List<EmployeeViewModel>();
            int pageNo = 1;
            if (param.iDisplayStart >= param.iDisplayLength)
            {
                pageNo = (param.iDisplayStart / param.iDisplayLength) + 1;
            }
            //int totalCount = 0;
            //if (param.sSearch != null)
            //{
            //    totalCount = db.Employees.Where(x => x.Name.Contains(param.sSearch) || x.department.dname.Contains(param.sSearch) || x.Address.Contains(param.sSearch)).Count();
            int totalCount = db.Employees.Count();    
            List = db.Employees.
                    OrderBy(x=>x.EmployeeId).Skip((pageNo-1)*param.iDisplayLength).Take(param.iDisplayLength).
                    Select(x => new EmployeeDomainModelWeb
                {
                    Name = x.Name,
                    dname = x.department.dname,
                    Address = x.Address,
                    EmployeeId = x.EmployeeId,
                    DId = x.department.DId,
                    IsDelete = x.IsDelete,
                    ExtraValue=5,
                    CurrentDate=DateTime.Now

                }).ToList();

            
            //else if (EName != null)
            //{
            //    totalCount = db.Employees.Where(x => x.Name.Contains(EName)).Count();
            //    List = db.Employees.Where(x => x.Name.Contains(EName)).
            //        OrderBy(x => x.EmployeeId).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).
            //        Select(x => new EmployeeDomainModel
            //        {
            //            Name = x.Name,
            //            dname = x.department.dname,
            //            Address = x.Address,
            //            EmployeeId = x.EmployeeId,
            //            DId = x.department.DId,
            //            IsDelete = x.IsDelete
            //        }).ToList();


            //}
            //else
            //{
            //    totalCount = db.Employees.Count();
            //    List = db.Employees.OrderBy(x=>x.EmployeeId).Skip((pageNo-1)*param.iDisplayLength).Take(param.iDisplayLength).
            //        Select(x => new EmployeeDomainModel
            //    {
            //        Name = x.Name,
            //        dname = x.department.dname,
            //        Address = x.Address,
            //        EmployeeId = x.EmployeeId,
            //        DId = x.department.DId,
            //        IsDelete = x.IsDelete
            //    }).ToList();
            //}
            AutoMapper.Mapper.Map(List, empVMList);


            return Json(new
            {
                aaData = empVMList,
                sEcho = param.sEcho,
                iTotalDisplayRecords = totalCount,
                iTotalRecords = totalCount,
            }, JsonRequestBehavior.AllowGet); ;
        }
        public JsonResult SendOTP()
        {
            int otpValue = new Random().Next(100000, 999999);
            var status = "";
            try
            {
                string recipient = System.Configuration.ConfigurationManager.AppSettings["RecipientNumber"].ToString();
                string APIKey = System.Configuration.ConfigurationManager.AppSettings["APIKey"].ToString();

                string message = "Your OTP Number is " + otpValue + " ( Sent By : Rishab Sharma )";
                String encodedMessage = HttpUtility.UrlEncode(message);

                using (var webClient = new WebClient())
                {
                    byte[] response = webClient.UploadValues("https://api.textlocal.in/send/", new NameValueCollection(){

                                         {"apikey" , APIKey},
                                         {"numbers" , recipient},
                                         {"message" , encodedMessage},
                                         {"sender" , "TXTLCL"}});

                    string result = System.Text.Encoding.UTF8.GetString(response);

                    var jsonObject = JObject.Parse(result);

                    status = jsonObject["status"].ToString();

                    Session["CurrentOTP"] = otpValue;
                }


                return Json(status, JsonRequestBehavior.AllowGet);


            }
            catch (Exception e)
            {

                throw (e);

            }

        }

        public ActionResult EnterOTP()
        {
            return View();
        }

        [HttpPost]
        public JsonResult VerifyOTP(string otp)
        {
            bool result = false;

            string sessionOTP = Session["CurrentOTP"].ToString();

            if (otp == sessionOTP)
            {
                result = true;

            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}


