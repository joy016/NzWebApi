using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NzApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCatsName()
        {
            string[] cats = new string[] { "Boki", "Yugee", "Albie" };
            return Ok(cats);
        }
    }
}
