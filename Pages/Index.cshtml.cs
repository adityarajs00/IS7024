using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Neighborhood_Watch.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Task<HttpResponseMessage> task = HttpClient.GetAsync("");
            HttpResponseMessage response = task.Result;

            List<Record> records = new List<Record>();

            if (response != null)
            {
                Task<string> readString = response.Content.ReadAsStringAsync();
                string recordJson = readString.Result;
                records = records.FromJson(recordJson) 
            }

        }
    }
}