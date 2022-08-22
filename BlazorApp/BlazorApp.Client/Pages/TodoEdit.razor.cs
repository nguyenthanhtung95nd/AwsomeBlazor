using BlazorApp.Client.Services;
using BlazorApp.Models;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorApp.Client.Pages
{
    public partial class TodoEdit
    {
        [Parameter]
        public string id { set; get; }

        [Inject]
        private ITodoApiClient _todoApiClient { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        [Inject]
        private IToastService _toastService { get; set; }

        private TodoUpdateRequest Todo;

        protected async override Task OnInitializedAsync()
        {
            var todoDto = await _todoApiClient.GetTodoDetail(Convert.ToInt32(id));
            Todo = new TodoUpdateRequest();
            Todo.Name = todoDto.Name;
            todoDto.Priority = todoDto.Priority;
        }

        private async Task HandleSubmit(EditContext context)
        {
            var result = await _todoApiClient.UpdateTodo(Convert.ToInt32(id), Todo);
            if (result)
            {
                _toastService.ShowSuccess($"{Todo.Name} has been updated successfully.", "Success");
                _navigationManager.NavigateTo("/todo-list");
            }
            else
            {
                _toastService.ShowError($"An error occurred in progress. Please contact to administrator.", "Error");
            }
        }
    }
}
