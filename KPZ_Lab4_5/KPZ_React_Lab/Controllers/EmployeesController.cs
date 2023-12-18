using KPZ_React_Lab.ViewModels;
using KPZ_React_Lab.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using KPZ_React_Lab.Models;

namespace KPZ_React_Lab.Controllers
{
     [Route("api/[controller]")]
     [ApiController]
     public class EmployeesController : ControllerBase
     {
          private IEmployeesService _employeesService;
          private IMapper _mapper;

          public EmployeesController(IEmployeesService employeesService, IMapper mapper)
          {
               _employeesService = employeesService;
               _mapper = mapper;
          }

          [HttpPost]
          public EmployeeModel Create(EmployeeModel model)
          {
               var modelToCreate = _mapper.Map<EmployeeViewModel>(model);
               return _mapper.Map<EmployeeModel>(_employeesService.Create(modelToCreate));
          }

          [HttpPatch]
          public EmployeeModel Update(EmployeeModel model)
          {
               var modelToUpdate = _mapper.Map<EmployeeViewModel>(model);
               return _mapper.Map<EmployeeModel>(_employeesService.Update(modelToUpdate));
          }

          [HttpGet("{id}")]
          public EmployeeModel Get(int id)
          {
               return _mapper.Map<EmployeeModel>(_employeesService.Get(id));
          }

          [HttpGet]
          public List<EmployeeModel> GetAll()
          {
               return _mapper.Map<List<EmployeeModel>>(_employeesService.Get());
          }

          [HttpDelete("{id}")]
          public IActionResult Delete(int id)
          {
               _employeesService.Delete(id);

               return Ok();
          }
     }
}
