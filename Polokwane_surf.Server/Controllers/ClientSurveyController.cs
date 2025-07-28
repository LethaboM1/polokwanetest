//using Microsoft.AspNetCore.Mvc;
//using Polokwane_surf.Server.Data;



//[ApiController]
//[Route("api/[controller]")]
//public class ClientSurveyController : ControllerBase
//{
//    private readonly AppDbContext _context;

//    public ClientSurveyController(AppDbContext context)
//    {
//        _context = context;
//    }

//    [HttpPost("submit")]
//    public async Task<IActionResult> Submit([FromBody] ClientSurveyForm model)
//    {
//        try
//        {
//            model.SubmittedAt = DateTime.UtcNow;
//            _context.ClientSurveys.Add(model);
//            await _context.SaveChangesAsync();

//            return Ok(new { message = "Survey submitted successfully" });
//        }
//        catch (Exception ex)
//        {
//            return StatusCode(500, new
//            {
//                error = ex.Message,
//                inner = ex.InnerException?.Message,
//                stack = ex.StackTrace
//            });
//        }
//    }
//}

using Microsoft.AspNetCore.Mvc;
using Polokwane_surf.Server.Data;
using Polokwane_surf.Server.Models;

namespace Polokwane_surf.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientSurveyController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientSurveyController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> Submit([FromBody] ClientSurvey model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                model.SubmittedAt = DateTime.UtcNow;
                _context.ClientSurveys.Add(model);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Survey submitted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = ex.Message,
                    inner = ex.InnerException?.Message,
                    stack = ex.StackTrace
                });
            }
        }
    }
}
