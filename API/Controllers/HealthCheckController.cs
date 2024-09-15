using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AleksMediaEmailSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthCheckController : ControllerBase
    {
        // Public health check, no auth required
        [HttpGet("public")]
        [AllowAnonymous]
        public IActionResult PublicHealthCheck()
        {
            return Ok(new { status = "Healthy", message = "Public endpoint - No authentication required." });
        }

        // Authenticated health check, requires a valid JWT token
        [HttpGet("secure")]
        [Authorize]
        public IActionResult SecureHealthCheck()
        {
            return Ok(new { status = "Healthy", message = "Secure endpoint - Authentication required." });
        }
    }
}
