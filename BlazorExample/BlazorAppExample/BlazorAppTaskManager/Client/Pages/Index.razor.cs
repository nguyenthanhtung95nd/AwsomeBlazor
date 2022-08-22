using BlazorAppTaskManager.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorAppTaskManager.Client.Pages
{
    public partial class Index
    {
        [Inject] 
        public HttpClient Http { get; set; }
        private IList<TaskItem> Tasks;
        private string Error;
        private string NewTask;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                string requestUri = "api/TaskItems";
                Tasks = await
                    Http.GetFromJsonAsync<IList<TaskItem>>
                   (requestUri);
            }
            catch (Exception)
            {
                Error = "Error Encountered";
            };
        }

        private async Task CheckboxChecked(TaskItem task)
        {
            task.IsComplete = !task.IsComplete;

            string requestUri = $"api/TaskItems/{task.TaskItemId}";
            var response = await
                Http.PutAsJsonAsync<TaskItem>(requestUri, task);
            if (!response.IsSuccessStatusCode)
            {
                Error = response.ReasonPhrase;
            };
        }

        private async Task DeleteTask(TaskItem taskItem)
        {
            Tasks.Remove(taskItem);

            string requestUri =
                $"api/TaskItems/{taskItem.TaskItemId}";
            var response = await Http.DeleteAsync(requestUri);
            if (!response.IsSuccessStatusCode)
            {
                Error = response.ReasonPhrase;
            };
        }

        private async Task AddTask()
        {
            if (!string.IsNullOrWhiteSpace(NewTask))
            {
                TaskItem newTaskItem = new TaskItem
                {
                    TaskName = NewTask,
                    IsComplete = false
                };
                Tasks.Add(newTaskItem);

                string requestUri = "api/TaskItems";
                var response = await Http.PostAsJsonAsync(requestUri, newTaskItem);
                if (response.IsSuccessStatusCode)
                {
                    NewTask = string.Empty;
                    var task =
                        await response.Content.ReadFromJsonAsync
                            <TaskItem>();
                }
                else
                {
                    Error = response.ReasonPhrase;
                };
            };

        }
    }
}
