using LearningWebApi.Entity;

namespace LearningWebApi.Service.ApiDataService;

public interface IDataService
{
    Task<IEnumerable<Project>> GetProjects();
    Task<Project?> GetProject(int id);
    Task<Project> AddProject(Project project);
    Task UpdateProject(Project project);
    Task<Project> DeleteProject(int id);

    Task<IEnumerable<Ticket>> GetTickets();
    Task<Ticket?> GetTicket(int id);
    Task<Ticket> AddTicket(Ticket ticket);
    Task UpdateTicket(Ticket ticket);
    Task<Ticket> DeleteTicket(int id);
    Task<IEnumerable<Ticket>> GetTicketsByProjectId(int projectId);
}