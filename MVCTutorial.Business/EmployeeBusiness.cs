using MVCLearning.Domain;
using MVCTutorial.Business.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVCTutorial.Business
{
    public class EmployeeBusiness : IEmployeeBusiness
    {
        public EmployeeBusiness(int i)
        {

        }
            public List<EmployeeDomainModel> GetAllEmployee()
            {
                List<EmployeeDomainModel> list = new List<EmployeeDomainModel>();
                list.Add(new EmployeeDomainModel { Name = "Rishab", EmployeeId = 1 });
                list.Add(new EmployeeDomainModel { Name = "Ritesh", EmployeeId = 2 });
                list.Add(new EmployeeDomainModel { Name = "Vikas", EmployeeId = 3 });
                list.Add(new EmployeeDomainModel { Name = "Prashant", EmployeeId = 4 });
                list.Add(new EmployeeDomainModel { Name = "Deepak", EmployeeId = 5 });
                return list;
            }

            public string GetEmployeeName(int EmpId)
            {
                return "Rishab" + EmpId;
            }
        } 
}
