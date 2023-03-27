using MVCTutorial.Business;
using MVCTutorial.Business.Interface;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace MVCLearning
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<EmployeeBusiness>(new InjectionConstructor(0));
            container.RegisterType<IEmployeeBusiness, EmployeeBusiness>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}