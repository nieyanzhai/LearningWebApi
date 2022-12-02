using System.Text.Json;
using LearningWebApi.BlazorWasm.Pages.Components;
using LearningWebApi.Entity;
using LearningWebApi.Service.ApiDataService;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace LearningWebApi.BlazorWasm.Pages.Projects;

public partial class AllProjectsPage
{
    [Inject] public IDialogService DialogService { get; set; }
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
        Console.WriteLine("Getting Projects...");
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

    private async Task DeleteProject(Project project)
    {
        var parameters = new DialogParameters
        {
            {"ContentText", $"Do you really want to delete the project({project.Name})?"},
            {"ButtonText", "Delete"},
            {"ButtonColor", Color.Error}
        };
        var options = new DialogOptions { MaxWidth = MaxWidth.Medium };
        var dialog =DialogService.Show<DialogComponent>("Delete", parameters, options);
        var dialogResult = await dialog.Result;
        if(!dialogResult.Cancelled)
        {
            Console.WriteLine("Project Deleting...");
            var deletedProject = await DataService.DeleteProject(project.Id);
            Console.WriteLine(JsonSerializer.Serialize(deletedProject, _jsonSerializerOptions));
            _projects = await GetProjects();
            StateHasChanged();
        }
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