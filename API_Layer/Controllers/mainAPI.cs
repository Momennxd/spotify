using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Spotify_BusinessLayer;

namespace API_Layer.Controllers
{
    [Route("api/mainAPI")]
    [ApiController]
    public class mainAPI : ControllerBase
    {



        [HttpGet]
        public ActionResult<dynamic> GetAllSongs()
        {
            return Spotify_BusinessLayer.clsSong.GetNumberOfRows();

        }










    }
}
