using CallsForService;
using IncidentRecord;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using Neighborhood_Watch.ApiData;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace Neighborhood_Watch.Pages
{
    public class IndexModel : PageModel
    {
        List<Calls> Service = new List<Calls>();
        List<Incidents> incident = new List<Incidents>();

        static readonly HttpClient client = new HttpClient();
        private readonly ILogger<IndexModel> _logger;
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        } // Initialized the _logger field within the constructor

        public async Task OnGetAsync()
        {
            LoadApiData();
            Task<List<Incidents>> incidents = GetIncidentsData();
            incident = incidents.Result;
            ViewData["Incidents"] = incident;
            
            Task<List<Calls>> Services = GetCallsData();
            Service = Services.Result;
            ViewData["Services"] = Service;

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                // Define the URLs of the JSON resources
                string url1 = "https://data.cincinnati-oh.gov/resource/k59e-2pvf.json";
                string url2 = "https://data.cincinnati-oh.gov/resource/gexm-h6bt.json";

                // Create a list to store the results
                List<string> searchResults = new List<string>();
                List<string> searchResults2 = new List<string>();

                // Fetch and process data from the first URL
                var dataFromUrl1 = await FetchDataFromUrl(url1);
                searchResults.AddRange(dataFromUrl1);

                // Fetch and process data from the second URL
                var dataFromUrl2 = await FetchDataFromUrl(url2);
                searchResults2.AddRange(dataFromUrl2);

                // Filter the results based on the search term within the attributes
                SearchResults = dataFromUrl1.Where(item => item.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
                string combinedJsonArray = "[" + string.Join(",", SearchResults) + "]";
                var IncidentList = Incidents.FromJson(combinedJsonArray);
                ViewData["Incidents"] = IncidentList;

                SearchResults2 = dataFromUrl2.Where(item => item.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
                string combinedJsonArray2 = "[" + string.Join(",", SearchResults2) + "]";
                var CallList = Calls.FromJson(combinedJsonArray2);
                ViewData["Services"] = CallList;

                ViewData["SearchTerm"] = SearchTerm;
            }

        }

        public void LoadApiData()
        {
            using (StreamReader r = new StreamReader("ApiData/data.json"))
            {
                String json = r.ReadToEnd();
                List<StateDataModel> dataModel = JsonConvert.DeserializeObject<List<StateDataModel>>(json);
                StateRepository.allStates = dataModel;
            }
            
        }

        private async Task<List<Calls>> GetCallsData()
        {
            List<Calls> Services = new List<Calls>();
            return await Task.Run(async () =>
            {
                Task<HttpResponseMessage> task = client.GetAsync("https://data.cincinnati-oh.gov/resource/gexm-h6bt.json");
                HttpResponseMessage result = await task.ConfigureAwait(false);
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
                HttpResponseMessage result = await task.ConfigureAwait(false);
                Task<string> readString = result.Content.ReadAsStringAsync();
                string incidentJson = readString.Result;
                incident = Incidents.FromJson(incidentJson);

                return incident;
            }

                );
        }


        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        public List<string>? SearchResults { get; set; }
        public List<string>? SearchResults2 { get; set; }

        private async Task<List<string>> FetchDataFromUrl(string url)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    // Send an HTTP GET request to the URL
                    HttpResponseMessage response = await httpClient.GetAsync(url).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read the JSON data as a string
                        string jsonContent = await response.Content.ReadAsStringAsync();
                        // Handle JSON parsing errors
                        try
                        {
                            // Deserialize the JSON data into JArray
                            JArray data = JArray.Parse(jsonContent);

                            // Convert the JArray back to a JSON string
                            List<string> dataList = data.Select(item => item.ToString()).ToList();

                            return dataList;
                        }
                        catch (JsonReaderException ex)
                        {
                            _logger.LogError(ex, "Error parsing JSON response");
                            return new List<string>();
                        }
                    }
                    else
                    {

                        return new List<string>();
                    }
                }
            }
            catch (Exception) // Resolved warnings
            {
                return new List<string>();
            }
        }

}

}