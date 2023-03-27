using AutoMapper;
using MVCLearning.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace MVCLearning.Models.Infrastructure
{
    public class AutomapperWebProfile:AutoMapper.Profile
    {
        private static IMapper mapper;

        public AutomapperWebProfile()
        {
            CreateMap<EmployeeDomainModelWeb, EmployeeViewModel>()

            .ForMember(dest => dest.ExtraValue, opt=>opt.MapFrom(src => src.ExtraValue.Encrypt()))
            .ForMember(dest => dest.CurrentDate, opt=>opt.MapFrom(src => src.CurrentDate.ToString("MM/dd/yyy hh:mm")));
            CreateMap<EmployeeDomainModel, EmployeeViewModel>();
            //Reverse Mapping
            CreateMap<EmployeeViewModel, EmployeeDomainModelWeb>();
            CreateMap<EmployeeDomainModel, EmployeeViewModel>();


        }
        public static void Run()
        {
            AutoMapper.Mapper.Initialize(a =>
            { 
                a.AddProfile<AutomapperWebProfile>();
            });
            //var config = new MapperConfiguration(cfg => {
            //    cfg.AddProfile<AutomapperWebProfile>();
            //});
            //mapper = config.CreateMapper();

        }
        
    }
    public static class ExtensionMethod
    {
        public static string Encrypt(this Int32 num)
        {
            return "Rishab:" + num;
        }
    }
}