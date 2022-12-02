using System.Collections;
using System.Diagnostics;
using System.Net;
using LearningWebApi.Entity;
using RestSharp;

namespace LearningWebApi.Service.ApiDataService;

public class DataService : IDataService, IDisposable
{
    private readonly RestClient _restClient;

    public DataService(RestClient restClient)
    {
        _restClient = restClient;
        _restClient.AddDefaultHeader("api-version", "2.0");
    }

    public async Task<IEnumerable<Project>> GetProjects()
    {
        var request = new RestRequest("api/projects");

        var response = await _restClient.ExecuteAsync<IEnumerable<Project>>(request);
        if (response.IsSuccessful) return response.Data ?? Array.Empty<Project>();
        if (response.StatusCode == HttpStatusCode.Unauthorized) throw new UnauthorizedAccessException();
        throw new Exception(response.ErrorMessage);
    }


    public async Task<Project?> GetProject(int id)
    {
        var request = new RestRequest($"api/projects/{id}");
        var response = await _restClient.ExecuteAsync<Project>(request);
        if (response.IsSuccessful) return response.Data;
        if (response.StatusCode == HttpStatusCode.Unauthorized) throw new UnauthorizedAccessException();
        throw new Exception(response.ErrorMessage);
    }

    public async Task<Project> AddProject(Project project)
    {
        var request = new RestRequest("api/projects", Method.Post);
        request.AddJsonBody(project);
        var response = await _restClient.ExecuteAsync<Project>(request);
        if (response.IsSuccessful)
        {
            Debug.Assert(response.Data != null, "response.Data != null");
            return response.Data;
        }

        if (response.StatusCode == HttpStatusCode.Unauthorized) throw new UnauthorizedAccessException();
        throw new Exception(response.ErrorMessage);
    }

    public async Task UpdateProject(Project project)
    {
        var request = new RestRequest($"api/projects/{project.Id}", Method.Put);
        request.AddJsonBody(project);
        var response = await _restClient.ExecuteAsync(request);
        if (response.IsSuccessful) return;
        if (response.StatusCode == HttpStatusCode.Unauthorized) throw new UnauthorizedAccessException();
        throw new Exception(response.ErrorMessage);
    }

    public async Task<Project> DeleteProject(int id)
    {
        var request = new RestRequest($"api/projects/{id}", Method.Delete);
        var response = await _restClient.ExecuteAsync<Project>(request);
        if (response.IsSuccessful)
        {
            Debug.Assert(response.Data != null, "response.Data != null");
            return response.Data;
        }

        if (response.StatusCode == HttpStatusCode.Unauthorized) throw new UnauthorizedAccessException();
        throw new Exception(response.ErrorMessage);
    }

    public async Task<IEnumerable<Ticket>> GetTickets()
    {
        var request = new RestRequest("api/tickets");
        var response = await _restClient.ExecuteAsync<IEnumerable<Ticket>>(request);
        if (response.IsSuccessful) return response.Data ?? Array.Empty<Ticket>();
        if (response.StatusCode == HttpStatusCode.Unauthorized) throw new UnauthorizedAccessException();
        throw new Exception(response.ErrorMessage);
    }

    public async Task<Ticket?> GetTicket(int id)
    {
        var request = new RestRequest($"api/tickets/{id}");
        var response = await _restClient.ExecuteAsync<Ticket>(request);
        if (response.IsSuccessful) return response.Data;
        if (response.StatusCode == HttpStatusCode.Unauthorized) throw new UnauthorizedAccessException();
        throw new Exception(response.ErrorMessage);
    }

    public async Task<Ticket> AddTicket(Ticket ticket)
    {
        var request = new RestRequest("api/tickets", Method.Post);
        request.AddJsonBody(ticket);
        var response = await _restClient.ExecuteAsync<Ticket>(request);
        if (response.IsSuccessful)
        {
            Debug.Assert(response.Data != null, "response.Data != null");
            return response.Data;
        }

        if (response.StatusCode == HttpStatusCode.Unauthorized) throw new UnauthorizedAccessException();
        throw new Exception(response.ErrorMessage);
    }

    public async Task UpdateTicket(Ticket ticket)
    {
        var request = new RestRequest($"api/tickets/{ticket.Id}", Method.Put);
        request.AddJsonBody(ticket);
        var response = await _restClient.ExecuteAsync(request);
        if (response.IsSuccessful) return;
        if (response.StatusCode == HttpStatusCode.Unauthorized) throw new UnauthorizedAccessException();
        throw new Exception(response.ErrorMessage);
    }

    public async Task<Ticket> DeleteTicket(int id)
    {
        var request = new RestRequest($"api/tickets/{id}", Method.Delete);
        var response = await _restClient.ExecuteAsync<Ticket>(request);
        if (response.IsSuccessful)
        {
            Debug.Assert(response.Data != null, "response.Data != null");
            return response.Data;
        }

        if (response.StatusCode == HttpStatusCode.Unauthorized) throw new UnauthorizedAccessException();
        throw new Exception(response.ErrorMessage);
    }

    public async Task<IEnumerable<Ticket>> GetTicketsByProjectId(int projectId)
    {
        var request = new RestRequest($"api/projects/{projectId}/tickets");
        var response = await _restClient.ExecuteAsync<IEnumerable<Ticket>>(request);
        if (response.IsSuccessful) return response.Data ?? Array.Empty<Ticket>();
        if (response.StatusCode == HttpStatusCode.Unauthorized) throw new UnauthorizedAccessException();
        throw new Exception(response.ErrorMessage);
    }

    public void Dispose()
    {
        _restClient?.Dispose();
        GC.SuppressFinalize(this);
    }
}