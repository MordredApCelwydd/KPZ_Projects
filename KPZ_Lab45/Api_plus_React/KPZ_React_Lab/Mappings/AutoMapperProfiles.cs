using AutoMapper;
using KPZ_React_Lab.Models;
using KPZ_React_Lab.ViewModels;
using System.Numerics;

namespace KPZ_React_Lab.Mappings
{
     public class AutoMapperProfiles : Profile
     {
          public AutoMapperProfiles()
          {
               CreateMap<ProjectModel, ProjectViewModel>().ReverseMap();

               CreateMap<EmployeeModel, EmployeeViewModel>().ReverseMap();
          }
     }
}
