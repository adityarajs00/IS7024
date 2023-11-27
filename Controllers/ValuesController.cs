using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Neighborhood_Watch.ApiData;

namespace StateData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [Produces("application/json")]
        public IList<StateDataModel> Get()
        {
            return StateRepository.allStates;
        }

        [HttpGet("{code}")]
        [Produces("application/json")]
        public StateDataModel Get(string code) {
/*            return StateRepository.allStates.Where(cd => cd.)*/
return StateRepository.allStates[0];
        }
    }
}
