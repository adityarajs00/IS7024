using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Neighborhood_Watch.Pages
{
    public class SearchModel : PageModel
    {
        // logger set up (To log the exceptions for debugging purposes)
        private readonly ILogger<SearchModel> _logger;

        public SearchModel(ILogger<SearchModel> logger)
        {
            _logger = logger;
        }

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        public List<string>? SearchResults { get; set; }

        public async Task OnGet()
        {
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
            // Using specific exception types to provide meaningful error messages
            catch (HttpRequestException ex)
            {
                // Handle HTTP request errors
                _logger.LogError(ex, "Error in HTTP request");
                return new List<string>();
            }
            catch (TaskCanceledException ex)
            {
                // Handle timeout errors
                _logger.LogError(ex, "Request timed out");
                return new List<string>();
            }
            catch (Exception ex)
            {
                // Handle other unexpected errors
                _logger.LogError(ex, "An unexpected error occurred");
                return new List<string>();
            }
        }
    }
}
