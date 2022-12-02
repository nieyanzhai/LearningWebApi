using LearningWebApi.Entity;
using LearningWebApi.Infrastructure.Data.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace LearningWebApi.Api.Controllers.V2;

[ApiVersion("2.0")]
[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IDataRepository _dataRepository;

    public ProjectsController(IDataRepository dataRepository)
    {
        _dataRepository = dataRepository;
    }


    [HttpGet]
    [EnableQuery]
    public async Task<IActionResult> Get()
    {
        var projects = await _dataRepository.GetProjects();
        return Ok(projects);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var project = await _dataRepository.GetProject(id);
        if (project is null) return NotFound();
        return Ok(project);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Project project)
    {
        var newProject = await _dataRepository.AddProject(project);
        return CreatedAtAction(nameof(Get), new {id = newProject.Id}, newProject);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Project project)
    {
        if (id != project.Id) return BadRequest();
        if (await _dataRepository.GetProject(id) is null) return NotFound();
        await _dataRepository.UpdateProject(project);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var projectToDelete = await _dataRepository.GetProject(id);
        if (projectToDelete is null) return NotFound();
        await _dataRepository.DeleteProject(id);
        return Ok(projectToDelete);
    }
}