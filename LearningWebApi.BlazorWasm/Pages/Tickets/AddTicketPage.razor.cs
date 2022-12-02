using LearningWebApi.Entity;
using LearningWebApi.Service.ApiDataService;
using Microsoft.AspNetCore.Components;

namespace LearningWebApi.BlazorWasm.Pages.Tickets;

public partial class AddTicketPage
{
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public IDataService DataService { get; set; }
    [Parameter] public int projectId { get; set; }

    private Ticket _ticket = new();


    protected override void OnParametersSet()
    {
        _ticket.ProjectId = projectId;
    }

    private async Task OnValidSubmit()
    {
        var addTicket = await DataService.AddTicket(_ticket);
        if (addTicket != null)
        {
            Console.WriteLine("Ticket added successfully");
            NavigationManager.NavigateTo($"/projects/{projectId}/tickets");
        }
    }
}