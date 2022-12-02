using System.Text.Json;
using LearningWebApi.Entity;
using LearningWebApi.Service.ApiDataService;
using Microsoft.AspNetCore.Components;

namespace LearningWebApi.BlazorWasm.Pages;

public partial class AllProjects
{
    [Inject] public IDataService DataService { get; set; }
    private readonly JsonSerializerOptions _jsonSerializerOptions = new() {WriteIndented = true};
    private IEnumerable<Project> _projects;
    private bool _loading;


    protected override async Task OnInitializedAsync()
    {
        _loading = true;
        _projects = await GetProjects();
        _loading = false;
    }
    

    private async Task<IEnumerable<Project>> GetProjects()
    {
        var projects = (await DataService.GetProjects()).ToList();
        Console.WriteLine(JsonSerializer.Serialize(projects, _jsonSerializerOptions));
        return projects;
    }

    private async Task GetProject()
    {
        var project = await DataService.GetProject(4);
        Console.WriteLine(JsonSerializer.Serialize(project, _jsonSerializerOptions));
    }

    private async Task CreateProject()
    {
        var project = new Project
        {
            Name = "Test Project",
            Description = "Test Description",
        };
        var createdProject = await DataService.AddProject(project);
        Console.WriteLine(JsonSerializer.Serialize(createdProject, _jsonSerializerOptions));
    }

    private async Task UpdateProject()
    {
        var project = new Project
        {
            Id = 4,
            Name = "Test Project 4",
            Description = "Test Description 4",
        };
        await DataService.UpdateProject(project);
        Console.WriteLine("Project Updated");
    }

    private async Task DeleteProject()
    {
        var deletedProject = await DataService.DeleteProject(4);
        Console.WriteLine(JsonSerializer.Serialize(deletedProject, _jsonSerializerOptions));
    }


    private async Task GetTickets()
    {
        var tickets = await DataService.GetTickets();
        Console.WriteLine(JsonSerializer.Serialize(tickets, _jsonSerializerOptions));
    }

    private async Task GetTicket()
    {
        var ticket = await DataService.GetTicket(14);
        Console.WriteLine(JsonSerializer.Serialize(ticket, _jsonSerializerOptions));
    }

    private async Task CreateTicket()
    {
        var ticket = new Ticket
        {
            Title = "Test Ticket",
            Description = "Test Description",
            ProjectId = 8,
        };
        var createdTicket = await DataService.AddTicket(ticket);
        Console.WriteLine(JsonSerializer.Serialize(createdTicket, _jsonSerializerOptions));
    }

    private async Task UpdateTicket()
    {
        var ticket = new Ticket
        {
            Id = 14,
            Title = "Test Ticket 14",
            Description = "Test Description 14",
            ProjectId = 8,
        };
        await DataService.UpdateTicket(ticket);
        Console.WriteLine("Ticket Updated");
    }

    private async Task DeleteTicket()
    {
        var deletedTicket = await DataService.DeleteTicket(14);
        Console.WriteLine(JsonSerializer.Serialize(deletedTicket, _jsonSerializerOptions));
    }

    private async Task GetTicketsByProjectId()
    {
        var tickets = await DataService.GetTicketsByProjectId(8);
        Console.WriteLine(JsonSerializer.Serialize(tickets, _jsonSerializerOptions));
    }
}