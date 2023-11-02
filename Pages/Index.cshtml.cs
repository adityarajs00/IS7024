using IncidentRecord;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace Neighborhood_Watch.Pages
{
    public class IndexModel : PageModel
    {
        static readonly HttpClient client = new HttpClient();
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

            
            var task = client.GetAsync("https://data.cincinnati-oh.gov/resource/k59e-2pvf.json");
            HttpResponseMessage result = task.Result;
            List<Incidents> incident = new List<Incidents>();
            if (result.IsSuccessStatusCode)
            {
                Task<string> readString = result.Content.ReadAsStringAsync();
                string incidentJson = readString.Result;
    
                incident = Incidents.FromJson(incidentJson);
            }
            ViewData["Incidents"] = incident;
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
    }
}