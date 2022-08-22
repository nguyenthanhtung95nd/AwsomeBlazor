using BlazorApp.Client.Components;
using BlazorApp.Client.Services;
using BlazorApp.Client.Shared;
using BlazorApp.Models;
using BlazorApp.Models.SeedWork;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorApp.Client.Pages
{
    public partial class TodoList
    {
        [Inject]
        private ITodoApiClient _todoApiClient { set; get; }

        [Inject]
        private IUserApiClient _userApiClient { set; get; }

        private List<TodoDto> ListTodo;

        private TodoListSearch TodoListSearch { get; set; } = new TodoListSearch();

        private List<AssigneeDto> Assignees { get; set; } = new List<AssigneeDto>();

        protected Confirmation DeleteConfirmation { set; get; }

        protected AssignTodo AssignTodoDialog { set; get; }

        public MetaData MetaData { get; set; } = new MetaData();

        [CascadingParameter]
        private Error Error { set; get; }

        private int DeleteId { set; get; }

        protected override async Task OnInitializedAsync()
        {
            await GetTodo();
            await GetAssignees();
        }

        private async Task GetTodo()
        {
            try
            {
                var response = await _todoApiClient.GetTodoList(TodoListSearch);
                ListTodo = response.Items;
                MetaData = response.MetaData;
            }
            catch (Exception ex)
            {
                Error.ProcessError(ex);
            }
        }

        private async Task GetAssignees()
        {
            var response = await _userApiClient.GetAssignees();
            Assignees = response;
        }

        public async Task HandleSearchForm(TodoListSearch todoListSearch)
        {
            TodoListSearch = todoListSearch;
            await GetTodo();
        }

        public void OnDeleteTodo(int deleteId)
        {
            DeleteId = deleteId;
            DeleteConfirmation.Show();
        }

        public async Task OnConfirmDeleteTodo(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                await _todoApiClient.DeleteTodo(DeleteId);
                await GetTodo();
            }
        }

        public async Task AssignTodoSuccess(bool result)
        {
            if (result)
            {
                await GetTodo();
            }
        }

        public void OpenAssignPopup(int id)
        {
            AssignTodoDialog.Show(id);
        }

        private async Task SelectedPage(int page)
        {
            TodoListSearch.PageNumber = page;
            await GetTodo();
        }
    }
}
