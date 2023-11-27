using Microsoft.AspNetCore.Mvc.RazorPages;
using NeighborhoodWatch;

namespace Neighborhood_Watch.Pages;

public class OtherTeamPage : PageModel
{
    public void OnGet()
    {
        ViewData["error"] = null;
        Task<List<OtherTeamPageModel>> OtherTeamDataTask = GetOtherTeamData();
        List<OtherTeamPageModel> OtherTeamData = OtherTeamDataTask.Result;
        ViewData["othersdata"] = OtherTeamData;
    }
    private static readonly HttpClient HttpClient = new HttpClient();
    private async Task<List<OtherTeamPageModel>> GetOtherTeamData()
    {
        return await Task.Run(async () =>
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://xmlproject2023.azurewebsites.net/state"),
            };
            List<OtherTeamPageModel> OtherTeamDataList = null;
            try
            {
                var response = await HttpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                OtherTeamDataList = OtherTeamPageModel.FromJson(body);
            }
            catch (Exception ex)
            {
                ViewData["error"] =
                    "Other Team's API seems to be down try visiting \n https://xmlproject2023.azurewebsites.net/state";
            }

            return OtherTeamDataList;

        });
    }
}