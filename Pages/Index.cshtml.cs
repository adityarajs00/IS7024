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

        public void OnGet()
        {
            Task<List<Incidents>> incidents = GetIncidentsData();
            List<Incidents> incident = incidents.Result;
            ViewData["Incidents"] = incident;
            
            Task<List<Calls>> Services = GetCallsData();
            List<Calls> Service = Services.Result;
            ViewData["Services"] = Service;

            List<X> derivedList = MergeData(Service, incident);
            ViewData["derivedList"] = derivedList;

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

        private List<X> MergeData(List<Calls> services, List<Incidents> incident)
        {

            IDictionary<string, Incidents> cpdneighbourhood = new Dictionary <string, Incidents>();
            
            foreach (Incidents i in incident)
            {
                if (i.CpdNeighborhood != null)
                {
                    cpdneighbourhood[i.CpdNeighborhood] = i;
                }
            }

            IDictionary<string,List<X>> x = new IDictionary<string, List<X>>();

            foreach (Calls j in services)
            {
                if (cpdneighbourhood.ContainsKey(j.CpdNeighborhood))
                {
                    X temp = new X();   
                    temp.IncidentTypeDesc = j.IncidentTypeDesc;
                    temp.IncidentNo = j.District;
;                    temp.Instanceid = cpdneighbourhood[j.CpdNeighborhood].Instanceid;
                    x.Add(temp);
                }
            }

            return x;
        }

    }
}