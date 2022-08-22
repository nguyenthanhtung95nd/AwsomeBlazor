using BlazorApp.Shared.Enums;
using BlazorApp.Shared.SeedWork;
using BlazorApp.Shared.Ticket;
using BlazorApp.UI.Extensions;
using BlazorApp.UI.Services.Interfaces;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace BlazorApp.UI.Services
{
    public class TicketSevice : ITicketSevice
    {
        private readonly HttpClient _httpClient;

        public TicketSevice(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginatedList<TicketViewModel>> GetTickets(SearchTicketViewModel viewModel)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                ["PageNumber"] = viewModel.PageNumber.ToString(),
                ["PageSize"] = viewModel.PageSize.ToString()
            };

            if (!string.IsNullOrEmpty(viewModel.Title))
                queryStringParam.Add("Title", viewModel.Title);
            if (viewModel.Status.HasValue)
                queryStringParam.Add("Status", viewModel.Status.ToString());

            string url = QueryHelpers.AddQueryString("/api/Tickets", queryStringParam);

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var result = await response.ReadAsObjectAsync<PaginatedList<TicketViewModel>>();
            return result;
        }

        public async Task<TicketViewModel> GetTicketById(int id)
        {
            var response = await _httpClient.GetAsync($"/api/Tickets/{id}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var result = await response.ReadAsObjectAsync<TicketViewModel>();
            return result;
        }

        public async Task<bool> DeleteTicket(int id)
        {
            var result = await _httpClient.DeleteAsync($"/api/Tickets/{id}");
            return result.IsSuccessStatusCode;
        }
        public async Task<bool> UpdateTicketStatus(int id, Status status)
        {
            var response = await _httpClient.PutJsonContentAsync($"/api/Ticket/UpdateStatusTicket/{id}", status);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            var result = await response.ReadAsObjectAsync<bool>();
            return result;
        }

        public async Task<int> CreateTicket(CreateTicketViewModel model)
        {
            var response = await _httpClient.PostJsonContentAsync($"/api/Tickets", model);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException("Error in create ticket");
            }
            var result = await response.ReadAsObjectAsync<int>();
            return result;
        }

        public async Task<bool> UpdateTicket(int id, UpdateTicketViewModel model)
        {
            var response = await _httpClient.PutJsonContentAsync($"/api/Tickets/{id}", model);
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException("Error in create ticket");
            }
            return response.IsSuccessStatusCode;
        }
    }
}