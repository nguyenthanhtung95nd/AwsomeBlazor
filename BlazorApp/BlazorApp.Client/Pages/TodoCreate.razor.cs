using BlazorApp.Client.Services;
using BlazorApp.Models;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Client.Pages
{
    public partial class TodoCreate
    {
        [Inject]
        private ITodoApiClient _todoApiClient { set; get; }

        [Inject] 
        private NavigationManager _navigationManager { set; get; }

        [Inject]
        private IToastService _toastService { set; get; }

        public TodoCreateRequest TodoCreateRequest { get; set; } = new TodoCreateRequest();

        public async Task HanldeSubmit()
        {
            var result = await _todoApiClient.CreateTodo(TodoCreateRequest);
            if(result)
            {
                _toastService.ShowSuccess($"{TodoCreateRequest.Name} has been created successfully.", "Success");
                _navigationManager.NavigateTo("/todo-list");
            } else
            {
                _toastService.ShowSuccess($"{TodoCreateRequest.Name} has been created successfully.", "Success");
            }
        }
    }
}
