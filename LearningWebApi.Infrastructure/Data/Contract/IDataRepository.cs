using LearningWebApi.Entity;

namespace LearningWebApi.Infrastructure.Data.Contract;

public interface IDataRepository
{
    #region Projects
    
    Task<IEnumerable<Project>> GetProjects();
    Task<Project?> GetProject(int id);
    Task<Project> AddProject(Project project);
    Task UpdateProject(Project project);
    Task DeleteProject(int id);
    
    #endregion

    #region Tickets

    Task<IEnumerable<Ticket>> GetTickets();
    Task<Ticket?> GetTicket(int id);
    Task<IEnumerable<Ticket>> GetTicketsByProjectId(int projectId);
    Task<Ticket> AddTicket(Ticket ticket);
    Task UpdateTicket(Ticket ticket);
    Task DeleteTicket(int id);
    
    
    #endregion
}