using AleksMediaEmailSystem.API.Models;
using AleksMediaEmailSystem.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AleksMediaEmailSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailTemplateController : ControllerBase
    {
        private readonly IEmailTemplateService _emailTemplateService;

        public EmailTemplateController(IEmailTemplateService emailTemplateService)
        {
            _emailTemplateService = emailTemplateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmailTemplates()
        {
            var templates = await _emailTemplateService.GetAllTemplatesAsync();
            return Ok(templates);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTemplateById(int id)
        {
            var template = await _emailTemplateService.GetTemplateByIdAsync(id);
            if (template == null) return NotFound();
            return Ok(template);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTemplate([FromBody] EmailTemplate template)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _emailTemplateService.AddTemplateAsync(template);
            return CreatedAtAction(nameof(GetTemplateById), new { id = template.Id }, template);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTemplate(int id, [FromBody] EmailTemplate template)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingTemplate = await _emailTemplateService.GetTemplateByIdAsync(id);
            if (existingTemplate == null) return NotFound();

            existingTemplate.TemplateName = template.TemplateName;
            existingTemplate.Subject = template.Subject;
            existingTemplate.Body = template.Body;

            await _emailTemplateService.UpdateTemplateAsync(existingTemplate);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemplate(int id)
        {
            var existingTemplate = await _emailTemplateService.GetTemplateByIdAsync(id);
            if (existingTemplate == null) return NotFound();

            await _emailTemplateService.DeleteTemplateAsync(id);
            return NoContent();
        }
    }
}
