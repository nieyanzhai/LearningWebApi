using LearningWebApi.Entity;
using LearningWebApi.Service.ApiDataService;
using Microsoft.AspNetCore.Components;

namespace LearningWebApi.BlazorWasm.Pages.Projects;

public partial class EditProjectPage
{
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public IDataService DataService { get; set; }

    [Parameter] public int Id { get; set; }


    private Project _project = new();
    private bool _showAlert;
    private string _alertMessage;
   

    protected override async Task OnParametersSetAsync()
    {
        try
        {
            var project = await DataService.GetProject(Id);
            if (project != null) _project = project;
            else
            {
                _alertMessage = "Project not found";
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
        await DataService.UpdateProject(_project);
        NavigationManager.NavigateTo("/projects");
    }

    private void ToggleAlert(bool value) => _showAlert = value;
}