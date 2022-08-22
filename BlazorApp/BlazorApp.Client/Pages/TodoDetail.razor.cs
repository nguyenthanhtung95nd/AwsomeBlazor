using BlazorApp.Client.Services;
using BlazorApp.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Client.Pages
{
    public partial class TodoDetail
    {
        [Inject]
        private ITodoApiClient _todoApiClient { set; get; }

        [Parameter]
        public string id { get; set; }

        private TodoDto Todo { set; get; }

        protected async override Task OnInitializedAsync()
        {
            Todo = await _todoApiClient.GetTodoDetail(Convert.ToInt32(id));
        }
    }
}
