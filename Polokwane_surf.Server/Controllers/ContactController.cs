using Microsoft.AspNetCore.Mvc;
using Polokwane_surf.Server.Data;
using Polokwane_surf.Server.Models;
using Polokwane_surf.Server.Service;

namespace Polokwane_surf.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly EmailService _emailService;

        public ContactController(AppDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitContactForm([FromBody] ContactForm model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                model.SubmittedAt = DateTime.UtcNow;
                await _context.ContactForm.AddAsync(model);
                await _context.SaveChangesAsync();


                return Ok(new { message = "Contact form submitted successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Failed to process request: " + ex.Message,
                    inner = ex.InnerException?.Message,
                    stack = ex.StackTrace
                });
            }

        }
    }
}
