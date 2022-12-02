using Dapper;
using LearningWebApi.Api.Services.Data.Contract;
using LearningWebApi.Api.Services.Data.DbContext;
using LearningWebApi.Entity;

namespace LearningWebApi.Api.Services.Data.Repository;

public class PostgreSqlDataRepository : IDataRepository
{
    private const string PROJECTS = "Projects";
    private const string TICKETS = "Tickets";

    private readonly PostgreSqlDbContext _dbContext;

    public PostgreSqlDataRepository(PostgreSqlDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private static string Table(string table) => $"\"{table}\"";

    private static string Column(string column) => $"\"{column}\"";


    #region Projects

    public async Task<IEnumerable<Project>> GetProjects()
    {
        var query = $"SELECT * FROM {Table(PROJECTS)}";
        using var connection = _dbContext.CreateConnection();
        var projects = (await connection.QueryAsync<Project>(query)).ToList();
        return projects;
    }

    public async Task<Project?> GetProject(int id)
    {
        var query = $"select * from {Table(PROJECTS)} where {Column("Id")} = @Id";
        using var connection = _dbContext.CreateConnection();
        var project = await connection.QueryFirstOrDefaultAsync<Project>(query, new {Id = id});
        return project;
    }

    public async Task<Project> AddProject(Project project)
    {
        var query =
            $"insert into {Table(PROJECTS)} ({Column("Name")}, {Column("Description")}) " +
            $"values (@Name, @Description)" +
            $"RETURNING {Column("Id")};";
        using var connection = _dbContext.CreateConnection();
        var id = await connection.QueryFirstOrDefaultAsync<int>(query, project);
        project.Id = id;
        return project;
    }

    public async Task UpdateProject(Project project)
    {
        var query =
            $"update {Table(PROJECTS)} " +
            $"set {Column("Name")} = @Name, {Column("Description")} = @Description " +
            $"where {Column("Id")} = @Id";
        using var connection = _dbContext.CreateConnection();
        await connection.ExecuteAsync(query, project);
    }

    public async Task DeleteProject(int id)
    {
        var query = $"delete from {Table(PROJECTS)} where {Column("Id")} = @Id";
        using var connection = _dbContext.CreateConnection();
        await connection.ExecuteAsync(query, new {Id = id});
    }

    #endregion


    #region Tickets

    public async Task<IEnumerable<Ticket>> GetTickets()
    {
        var query = $"select * from {Table(TICKETS)}";
        using var connection = _dbContext.CreateConnection();
        var tickets = (await connection.QueryAsync<Ticket>(query)).ToList();
        return tickets;
    }

    public async Task<Ticket?> GetTicket(int id)
    {
        var query = $"select * from {Table(TICKETS)} where {Column("Id")} = @Id";
        using var connection = _dbContext.CreateConnection();
        var ticket = await connection.QueryFirstOrDefaultAsync<Ticket>(query, new {Id = id});
        return ticket;
    }

    public async Task<IEnumerable<Ticket>> GetTicketsByProjectId(int projectId)
    {
        var query = $"select * from {Table(TICKETS)} where {Column("ProjectId")} = @ProjectId";
        using var connection = _dbContext.CreateConnection();
        var tickets = (await connection.QueryAsync<Ticket>(query, new {ProjectId = projectId})).ToList();
        return tickets;
    }

    public async Task<Ticket> AddTicket(Ticket ticket)
    {
        var query =
            $"insert into " +
            $"{Table(TICKETS)} ({Column("Title")}, {Column("Description")} , {Column("Owner")} , {Column("DueTo")} , {Column("CreatedAt")}, {Column("ProjectId")}) " +
            $"values (@Title, @Description, @Owner, @DueTo, @CreatedAt, @ProjectId)" +
            $"RETURNING {Column("Id")};";
        using var connection = _dbContext.CreateConnection();
        var id = await connection.QueryFirstOrDefaultAsync<int>(query, ticket);
        ticket.Id = id;
        return ticket;
    }

    public async Task UpdateTicket(Ticket ticket)
    {
        var query =
            $"update {Table(TICKETS)} " +
            $"set {Column("Title")}  = @Title,{Column("Description")}  = @Description,{Column("Owner")}  = @Owner,{Column("DueTo")}  = @DueTo,{Column("CreatedAt")}  = @CreatedAt,{Column("ProjectId")}  = @ProjectId " +
            $"where {Column("Id")} = @Id";
        using var connection = _dbContext.CreateConnection();
        await connection.ExecuteAsync(query, ticket);
    }

    public async Task DeleteTicket(int id)
    {
        var query = $"delete from {Table(TICKETS)} where {Column("Id")} = @Id";
        using var connection = _dbContext.CreateConnection();
        await connection.ExecuteAsync(query, new {Id = id});
    }

    #endregion
}