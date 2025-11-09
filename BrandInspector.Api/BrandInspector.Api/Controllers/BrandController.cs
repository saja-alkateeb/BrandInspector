using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BrandInspector.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("brand")]
    public class BrandController : ControllerBase
    {
        [HttpGet("fonts")]
        public IActionResult GetFonts()
        {
            var fonts = new List<string> { "Arial", "Calibri", "Roboto", "Times New Roman" };
            return Ok(fonts);
        }

        [HttpGet("colors")]
        public IActionResult GetColors()
        {
            var colors = new List<string> { "#000000", "#FFFFFF", "#0056B3", "#FF5733" };
            return Ok(colors);
        }

        [HttpGet("sizes")]
        public IActionResult GetSizes()
        {
            var sizes = new List<double> { 10, 12, 14, 16, 18, 20 };
            return Ok(sizes);
        }
    }
}
