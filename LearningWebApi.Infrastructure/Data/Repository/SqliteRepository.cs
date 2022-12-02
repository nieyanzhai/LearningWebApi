using Dapper;
using LearningWebApi.Api.Services.Data.Contract;
using LearningWebApi.Api.Services.Data.DbContext;
using LearningWebApi.Entity;

namespace LearningWebApi.Api.Services.Data.Repository;

public class SqliteDataRepository : IDataRepository
{
    private readonly SqliteDbContext _dbContext;

    public SqliteDataRepository(SqliteDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    #region Projects

    public async Task<IEnumerable<Project>> GetProjects()
    {
        var query = "select * from Projects";
        using var connection = _dbContext.CreateConnection();
        var projects = (await connection.QueryAsync<Project>(query)).ToList();
        return projects;
    }

    public async Task<Project?> GetProject(int id)
    {
        var query = "select * from Projects where Id = @Id";
        using var connection = _dbContext.CreateConnection();
        var project = await connection.QueryFirstOrDefaultAsync<Project>(query, new {Id = id});
        return project;
    }

    public async Task<Project> AddProject(Project project)
    {
        var query = "insert into Projects (Name, Description) values (@Name, @Description); select last_insert_rowid()";
        using var connection = _dbContext.CreateConnection();
        var id = await connection.QueryFirstOrDefaultAsync<int>(query, project);
        project.Id = id;
        return project;
    }

    public async Task UpdateProject(Project project)
    {
        var query = "update Projects set Name = @Name, Description = @Description where Id = @Id";
        using var connection = _dbContext.CreateConnection();
        await connection.ExecuteAsync(query, project);
    }

    public async Task DeleteProject(int id)
    {
        var query = "delete from Projects where Id = @Id";
        using var connection = _dbContext.CreateConnection();
        await connection.ExecuteAsync(query, new {Id = id});
    }

    #endregion


    #region Tickets

    public async Task<IEnumerable<Ticket>> GetTickets()
    {
        var query = "select * from Tickets";
        using var connection = _dbContext.CreateConnection();
        var tickets = (await connection.QueryAsync<Ticket>(query)).ToList();
        return tickets;
    }

    public async Task<Ticket?> GetTicket(int id)
    {
        var query = "select * from Tickets where Id = @Id";
        using var connection = _dbContext.CreateConnection();
        var ticket = await connection.QueryFirstOrDefaultAsync<Ticket>(query, new {Id = id});
        return ticket;
    }

    public async Task<IEnumerable<Ticket>> GetTicketsByProjectId(int projectId)
    {
        var query = "select * from Tickets where ProjectId = @ProjectId";
        using var connection = _dbContext.CreateConnection();
        var tickets = (await connection.QueryAsync<Ticket>(query, new {ProjectId = projectId})).ToList();
        return tickets;
    }

    public async Task<Ticket> AddTicket(Ticket ticket)
    {
        var query =
            "insert into " +
            "Tickets (Title, Description, Owner, DueTo, CreatedAt, ProjectId) " +
            "values (@Title, @Description, @Owner, @DueTo, @CreatedAt, @ProjectId); " +
            "select last_insert_rowid()";
        using var connection = _dbContext.CreateConnection();
        var id = await connection.QueryFirstOrDefaultAsync<int>(query, ticket);
        ticket.Id = id;
        return ticket;
    }

    public async Task UpdateTicket(Ticket ticket)
    {
        var query =
            "update Tickets " +
            "set Title = @Title, Description = @Description, Owner = @Owner, DueTo = @DueTo, CreatedAt = @CreatedAt, ProjectId = @ProjectId " +
            "where Id = @Id";
        using var connection = _dbContext.CreateConnection();
        await connection.ExecuteAsync(query, ticket);
    }

    public async Task DeleteTicket(int id)
    {
        var query = "delete from Tickets where Id = @Id";
        using var connection = _dbContext.CreateConnection();
        await connection.ExecuteAsync(query, new {Id = id});
    }

    #endregion
}