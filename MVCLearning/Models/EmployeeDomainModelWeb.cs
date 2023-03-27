using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCLearning.Models
{
    public class EmployeeDomainModelWeb
    {
        public int EmployeeId { get; set; }
        //[Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }
       // [Required(ErrorMessage = "Enter Address")]

        public string Address { get; set; }
       // [Required(ErrorMessage = "Enter Department")]
        public Nullable<int> DId { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string dname { get; set; }
        public bool remember { get; set; }
        public string SiteName { get; set; }
        public int ExtraValue { get; set; }
        public DateTime CurrentDate { get; set; }
    }
}