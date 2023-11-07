using CallsForService;
using IncidentRecord;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Neighborhood_Watch.Pages
{
    public class IndexModel : PageModel
    {
        static readonly HttpClient client = new HttpClient();
        private readonly ILogger<IndexModel> _logger;

        public async Task OnGetAsync()
        {
            Task<List<Incidents>> incidents = GetIncidentsData();
            List<Incidents> incident = incidents.Result;
            ViewData["Incidents"] = incident;
            
            Task<List<Calls>> Services = GetCallsData();
            List<Calls> Service = Services.Result;
            ViewData["Services"] = Service;

            //List<X> derivedList = MergeData(Service, incident);
            //ViewData["derivedList"] = derivedList;

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                // Define the URLs of the JSON resources
                string url1 = "https://data.cincinnati-oh.gov/resource/k59e-2pvf.json";
                string url2 = "https://data.cincinnati-oh.gov/resource/gexm-h6bt.json";

                // Create a list to store the results
                List<string> searchResults = new List<string>();

                // Fetch and process data from the first URL
                var dataFromUrl1 = await FetchDataFromUrl(url1);
                searchResults.AddRange(dataFromUrl1);

                // Fetch and process data from the second URL
                var dataFromUrl2 = await FetchDataFromUrl(url2);
                searchResults.AddRange(dataFromUrl2);

                // Filter the results based on the search term within the attributes
                SearchResults = searchResults.Where(item => item.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }

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

                return incident;
            }

                );
        }

        //private List<X> MergeData(List<Calls> services, List<Incidents> incident)
        //{

        //    IDictionary<string, Incidents> cpdneighbourhood = new Dictionary<string, Incidents>();

        //    foreach (Incidents i in incident)
        //    {
        //        if (i.CpdNeighborhood != null)
        //        {
        //            cpdneighbourhood[i.CpdNeighborhood] = i;
        //        }
        //    }

        //    IDictionary<string, List<X>> x = new IDictionary<string, List<X>>();

        //    foreach (Calls j in services)
        //    {
        //        if (cpdneighbourhood.ContainsKey(j.CpdNeighborhood))
        //        {
        //            X temp = new X();
        //            temp.IncidentTypeDesc = j.IncidentTypeDesc;
        //            temp.IncidentNo = j.District;
        //            ; temp.Instanceid = cpdneighbourhood[j.CpdNeighborhood].Instanceid;
        //            x.Add(temp);
        //        }
        //    }

        //    return x;
        //}

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        public List<string>? SearchResults { get; set; }

        private async Task<List<string>> FetchDataFromUrl(string url)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    // Send an HTTP GET request to the URL
                    HttpResponseMessage response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read the JSON data as a string
                        string jsonContent = await response.Content.ReadAsStringAsync();

                        // Deserialize the JSON data into JArray
                        JArray data = JArray.Parse(jsonContent);

                        // Convert the JArray back to a JSON string
                        List<string> dataList = data.Select(item => item.ToString()).ToList();

                        return dataList;
                    }
                    else
                    {

                        return new List<string>();
                    }
                }
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

}

}