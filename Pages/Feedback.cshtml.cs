using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Neighborhood_Watch.Pages
{

    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }
        public List<FormData> NeighborhoodData { get; private set; }
        public void OnGet()
        {
            NeighborhoodData = GetNeighborhoodData();
        }

        private List<FormData> GetNeighborhoodData()
        {
            // Specify the path to your JSON file
            string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "neighborhood_data.json");

            // Read JSON data from the file
            return System.IO.File.Exists(jsonFilePath)
                ? JsonConvert.DeserializeObject<List<FormData>>(System.IO.File.ReadAllText(jsonFilePath))
                : new List<FormData>();
        }




        public IActionResult OnPost()
        {
            // Retrieve form data from the request
            var formData = new FormData
            {
                FullName = Request.Form["fullName"],
                Email = Request.Form["email"],
                PhoneNumber = Request.Form["phoneNumber"],
                Address = Request.Form["address"],
                City = Request.Form["city"],
                Neighborhood = Request.Form["neighborhood"],
                SafetyRating = Convert.ToInt32(Request.Form["safety_rating"]),
                Comments = Request.Form["comments"]
            };

            // Save form data to a JSON file
            SaveToJsonFile(formData);

            // You can perform additional processing or redirect the user after saving the data
            return RedirectToPage("/Feedback"); // Redirect to the home page for example
        }

        private void SaveToJsonFile(FormData formData)
        {
            // Specify the path to your JSON file
            string jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "neighborhood_data.json");

            // Read existing JSON data, or create an empty list if the file doesn't exist
            List<FormData> existingData = System.IO.File.Exists(jsonFilePath)
             ? JsonConvert.DeserializeObject<List<FormData>>(System.IO.File.ReadAllText(jsonFilePath))
             : new List<FormData>();


            // Add the new form data to the list
            existingData.Add(formData);

            // Write the updated list back to the JSON file
            System.IO.File.WriteAllText(jsonFilePath, JsonConvert.SerializeObject(existingData, Formatting.Indented));
        }
    }
}




