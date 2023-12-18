using KPZ_React_Lab.Services.Interfaces;
using KPZ_React_Lab.ViewModels;
using KPZ_React_Lab.Models;
using KPZ_React_Lab.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace KPZ_Lab_React.Controllers
{
     [Route("api/[controller]")]
     [ApiController]
     public class ProjectsController : ControllerBase
     {
          private IProjectsService _productsService;
          IMapper _mapper;

          public ProjectsController(IProjectsService productsService, IMapper mapper)
          {
               _productsService = productsService;
               _mapper = mapper;
          }

          [HttpPost]
          public ProjectModel Create(ProjectModel model)
          {
               var modelToCreate = _mapper.Map<ProjectViewModel>(model);
               return _mapper.Map<ProjectModel>(_productsService.Create(modelToCreate));
          }

          [HttpPatch]
          public ProjectModel Update(ProjectModel model)
          {
               var modelToUpdate = _mapper.Map<ProjectViewModel>(model);
               return _mapper.Map<ProjectModel>(_productsService.Update(modelToUpdate));
          }

          [HttpGet("{id}")]
          public ProjectModel Get(int id)
          {
               return _mapper.Map<ProjectModel>(_productsService.Get(id));
          }

          [HttpGet]
          public List<ProjectModel> GetAll()
          {
               return _mapper.Map<List<ProjectModel>>(_productsService.Get());
          }

          [HttpDelete("{id}")]
          public IActionResult Delete(int id)
          {
               _productsService.Delete(id);

               return Ok();
          }
     }
}
