using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Neighborhood_Watch.Pages
{
    public class SearchModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        public List<string>? SearchResults { get; set; }

        public async Task OnGet()
        {
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                // Define the URLs of the JSON resources
                string neighboorhoodURL1 = "https://data.cincinnati-oh.gov/resource/k59e-2pvf.json";
                string neighboorhoodURL2 = "https://data.cincinnati-oh.gov/resource/gexm-h6bt.json";

                // Create a list to store the results
                List<string> searchResults = new List<string>();

                // Fetch and process data from the first URL
                var neighborhoodDataFromUrl1 = await FetchDataFromUrl(neighboorhoodURL1);
                searchResults.AddRange(neighborhoodDataFromUrl1);

                // Fetch and process data from the second URL
                var neighborhoodDataFromUrl2 = await FetchDataFromUrl(neighboorhoodURL2);
                searchResults.AddRange(neighborhoodDataFromUrl2);

                // Filter the results based on the search term within the attributes
                SearchResults = searchResults.Where(item => item.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }

        private async Task<List<string>> FetchDataFromUrl(string url)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    // Send an HTTP GET request to the URL
                    HttpResponseMessage response = await httpClient.GetAsync(url);

                    if (response != null && response.IsSuccessStatusCode)
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
