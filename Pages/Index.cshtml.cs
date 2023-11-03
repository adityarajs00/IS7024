
using CallsForService;
using IncidentRecord;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace Neighborhood_Watch.Pages
{
    public class IndexModel : PageModel
    {
        static readonly HttpClient client = new HttpClient();
        private readonly ILogger<IndexModel> _logger;
        static readonly HttpClient client = new HttpClient();
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {


            Task<List<Incidents>> incidents = GetIncidentsData();
            List<Incidents> incident = incidents.Result;
            ViewData["Incidents"] = incident;
            
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
        private async Task<List<Incidents>> GetIncidentsData()
        {
            List<Incidents> incident = new List<Incidents>();
            return await Task.Run(async () =>
            {
                Task<HttpResponseMessage> task = client.GetAsync("https://data.cincinnati-oh.gov/resource/k59e-2pvf.json");
                HttpResponseMessage result = await task;
                Task<string> readString = result.Content.ReadAsStringAsync();
                string incidentJson = readString.Result;
                incident = Incidents.FromJson(incidentJson);
                
                //Joining the data sets
                Task<HttpResponseMessage> test_response = client.GetAsync("https://data.cincinnati-oh.gov/resource/k59e-2pvf.json");
                HttpResponseMessage Test_Response = await test_response;
                Task<string> ResponseStringTask = Test_Response.Content.ReadAsStringAsync();
                string Response_Test_Json = ResponseStringTask.Result;
                List<Responses> test_responses_1 = Responses.FromJson(Response_Test_Json);

                IDictionary<string, Responses> cpdneighbourhood = new Dictionary<string, Responses>();
                foreach (Responses t_response in test_responses_1)
                {
                    cpdneighbourhood[t_response.CpdNeighborhood] = t_response;
                }
                List<Incidents> test_incidents = new List<Incidents>();
                foreach (Incidents t_incidents in incident)
                {
                    if (cpdneighbourhood.ContainsKey(t_incidents.CpdNeighborhood))
                    {
                        test_incidents.Add(t_incidents);
                    }
                }

                return test_incidents;
                // return incident;(This is for Incident class)
            }

                );
        }
       
    }
}