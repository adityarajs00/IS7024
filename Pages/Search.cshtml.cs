using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Neighborhood_Watch.Pages
{
    public class SearchModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string ? SearchTerm { get; set; }

        public List<string> ? SearchResults { get; set; }

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(SearchTerm))
            {
                // Implement your search logic here.
                // For example, searching a list of items.
                var allItems = new List<string> { "Item 1", "Item 2", "Item 3" };
                SearchResults = allItems.Where(item => item.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }
    }
}
