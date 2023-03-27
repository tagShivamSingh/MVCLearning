using MVCLearning.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVCTutorial.Business.Interface
{
    public interface IEmployeeBusiness
    {
        string GetEmployeeName (int EmpId);
        List<EmployeeDomainModel> GetAllEmployee();
    }
}
