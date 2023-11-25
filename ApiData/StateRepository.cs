namespace Neighborhood_Watch.ApiData
{
    public class StateRepository
    {
        static StateRepository()
        {
            allStates = new List<StateDataModel>();
        }

        public static IList<StateDataModel> allStates { get; set; }
    }
    
}
