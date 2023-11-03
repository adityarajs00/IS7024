using CallsForService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Neighborhood_Watch.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        static readonly HttpClient client = new HttpClient();
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

            Task<List<Calls>> Services = GetCallsData();
            List<Calls> Service = Services.Result;
            ViewData["Services"] = Service;

            /*var task = client.GetAsync("https://data.cincinnati-oh.gov/resource/k59e-2pvf.json");
            HttpResponseMessage result = task.Result;
            List<Incidents> incident = new List<Incidents>();
            if (result.IsSuccessStatusCode)
            {
                Task<string> readString = result.Content.ReadAsStringAsync();
                string incidentJson = readString.Result;
                incident = Incidents.FromJson(incidentJson);
            }
            ViewData["Incidents"] = incident;*/

            /*Task<HttpResponseMessage> task = HttpClient.GetAsync("");
            HttpResponseMessage response = task.Result;
 
            List<Record> records = new List<Record>();
 
            if (response != null)
            {
                Task<string> readString = response.Content.ReadAsStringAsync();
                string recordJson = readString.Result;
                records = records.FromJson(recordJson); 
            }
           */

        }
        private async Task<List<Calls>> GetCallsData()
        {
            List<Calls> Services = new List<Calls>();
            return await Task.Run(async () =>
            {
                Task<HttpResponseMessage> task = client.GetAsync("https://data.cincinnati-oh.gov/resource/gexm-h6bt.json");
                HttpResponseMessage result = await task;
                Task<string> readString = result.Content.ReadAsStringAsync();
                string ServicesJson = readString.Result;
                Services = Calls.FromJson(ServicesJson);
                return Services;
            }

                );
        }
    }
}