using LearningWebApi.Entity;
using LearningWebApi.Infrastructure.Data.Contract;
using Microsoft.AspNetCore.Mvc;

namespace LearningWebApi.Api.Controllers.V1;

[ApiVersion("1.0")]
[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly IDataRepository _repository;

    public TicketsController(IDataRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetTickets()
    {
        var tickets = await _repository.GetTickets();
        return Ok(tickets);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTicket([FromRoute] int id)
    {
        var ticket = await _repository.GetTicket(id);
        if (ticket == null) return NotFound();
        return Ok(ticket);
    }

    [HttpGet("/api/projects/{projectId}/tickets")]
    public async Task<IActionResult> GetTicketsByProjectId([FromRoute] int projectId)
    {
        var tickets = await _repository.GetTicketsByProjectId(projectId);
        return Ok(tickets);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTicket([FromBody] Ticket ticket)
    {
        try
        {
            var newTicket = await _repository.AddTicket(ticket);
            return CreatedAtAction(nameof(GetTicket), new {id = newTicket.Id}, newTicket);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTicket([FromRoute] int id, [FromBody] Ticket ticket)
    {
        if (id != ticket.Id) return BadRequest();
        var ticketToUpdate = await _repository.GetTicket(id);
        if (ticketToUpdate == null) return NotFound();
        await _repository.UpdateTicket(ticket);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTicket([FromRoute] int id)
    {
        var ticketToDelete = await _repository.GetTicket(id);
        if (ticketToDelete == null) return NotFound();
        await _repository.DeleteTicket(id);
        return Ok(ticketToDelete);
    }
}