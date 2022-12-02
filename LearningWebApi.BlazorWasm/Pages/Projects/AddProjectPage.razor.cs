using LearningWebApi.Entity;
using LearningWebApi.Service.ApiDataService;
using Microsoft.AspNetCore.Components;

namespace LearningWebApi.BlazorWasm.Pages.Projects;

public partial class AddProjectPage
{
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public IDataService DataService { get; set; }

    private Project _project = new();

    private async Task OnValidSubmit()
    {
        var addedProject = await DataService.AddProject(_project);
        if (addedProject != null)
        {
            Console.WriteLine("Project added successfully");
            NavigationManager.NavigateTo("/projects");
        }
    }
}