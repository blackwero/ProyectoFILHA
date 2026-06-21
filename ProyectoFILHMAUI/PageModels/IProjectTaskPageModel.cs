using CommunityToolkit.Mvvm.Input;
using ProyectoFILHMAUI.Models;

namespace ProyectoFILHMAUI.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}