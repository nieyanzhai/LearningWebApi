using System.Text.Json;
using LearningWebApi.BlazorWasm.Pages.Components;
using LearningWebApi.Entity;
using LearningWebApi.Service.ApiDataService;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace LearningWebApi.BlazorWasm.Pages.Tickets;

public partial class AllTicketsPage
{
    [Inject] public IDialogService DialogService { get; set; }
    [Inject] public IDataService DataService { get; set; }
    [Parameter] public int projectId { get; set; }
    private readonly JsonSerializerOptions _jsonSerializerOptions = new() {WriteIndented = true};

    private IEnumerable<Ticket> _tickets;
    private bool _loading;

    protected override async Task OnParametersSetAsync()
    {
        _loading = true;
        _tickets = await GetTickets();
        _loading = false;
    }


    private async Task<IEnumerable<Ticket>> GetTickets()
    {
        Console.WriteLine("Getting Tickets...");
        var tickets = (await DataService.GetTicketsByProjectId(projectId)).ToList();
        Console.WriteLine(JsonSerializer.Serialize(tickets, _jsonSerializerOptions));
        return tickets;
    }

    private async Task DeleteTicket(Ticket ticket)
    {
        var parameters = new DialogParameters
        {
            {"ContentText", $"Do you really want to delete the ticket({ticket.Title})?"},
            {"ButtonText", "Delete"},
            {"ButtonColor", Color.Error}
        };
        var options = new DialogOptions {MaxWidth = MaxWidth.Medium};
        var dialog = DialogService.Show<DialogComponent>("Delete", parameters, options);
        var dialogResult = await dialog.Result;
        if (!dialogResult.Cancelled)
        {
            Console.WriteLine("Ticket Deleting...");
            var deletedProject = await DataService.DeleteTicket(ticket.Id);
            Console.WriteLine(JsonSerializer.Serialize(deletedProject, _jsonSerializerOptions));
            _tickets = await GetTickets();
            StateHasChanged();
        }
    }
}