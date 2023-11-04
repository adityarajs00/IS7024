using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Neighborhood_Watch.Pages
{
    public class SearchResultsModel : PageModel
    {
        public string? SearchTerm { get; set; }
        public List<string>? SearchResults { get; set; }

        public void OnGet()
        {
            // The OnGet method in this page doesn't need to contain the data processing logic.
            // It should be used to display the results passed as parameters.
        }
    }

}

