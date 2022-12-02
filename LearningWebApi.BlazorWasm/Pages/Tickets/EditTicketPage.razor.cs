using LearningWebApi.Entity;
using LearningWebApi.Service.ApiDataService;
using Microsoft.AspNetCore.Components;

namespace LearningWebApi.BlazorWasm.Pages.Tickets;

public partial class EditTicketPage
{
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public IDataService DataService { get; set; }

    [Parameter] public int projectId { get; set; }
    [Parameter] public int ticketId { get; set; }


    private Ticket _ticket = new();
    private bool _showAlert;
    private string _alertMessage;


    protected override async Task OnParametersSetAsync()
    {
        try
        {
            var ticket = (await DataService.GetTicketsByProjectId(projectId)).FirstOrDefault(t => t.Id == ticketId);
            if (ticket != null) _ticket = ticket;
            else
            {
                _alertMessage = "Ticket not found";
                ToggleAlert(true);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _alertMessage = e.Message;
            ToggleAlert(true);
        }
    }

    private async Task OnValidSubmit()
    {
        await DataService.UpdateTicket(_ticket);
        NavigationManager.NavigateTo($"/projects/{projectId}/tickets");
    }

    private void ToggleAlert(bool value) => _showAlert = value;
}