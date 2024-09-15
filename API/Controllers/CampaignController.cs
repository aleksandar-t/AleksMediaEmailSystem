using AleksMediaEmailSystem.API.Models;
using AleksMediaEmailSystem.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AleksMediaEmailSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignService _campaignService;

        public CampaignController(ICampaignService campaignService)
        {
            _campaignService = campaignService;
        }

        [HttpPost("TriggerCampaign")]
        public async Task<IActionResult> TriggerCampaign([FromBody] TriggerCampaign campaign)
        {
           await _campaignService.TriggerCampaignFromXmlFileAsync(campaign.XmlFilePath);

            return Ok("The campaign was queued successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCampaigns()
        {
            var campaigns = await _campaignService.GetAllCampaignsAsync();
            return Ok(campaigns);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCampaignById(int id)
        {
            var campaign = await _campaignService.GetCampaignByIdAsync(id);
            if (campaign == null) return NotFound();
            return Ok(campaign);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCampaign([FromBody] Campaign campaign)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _campaignService.AddCampaignAsync(campaign);
            return CreatedAtAction(nameof(GetCampaignById), new { id = campaign.Id }, campaign);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCampaign(int id, [FromBody] Campaign campaign)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var existingCampaign = await _campaignService.GetCampaignByIdAsync(id);
            if (existingCampaign == null) return NotFound();

            existingCampaign.CampaignName = campaign.CampaignName;
            existingCampaign.StartDate = campaign.StartDate;
            existingCampaign.EndDate = campaign.EndDate;

            await _campaignService.UpdateCampaignAsync(existingCampaign);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCampaign(int id)
        {
            var existingCampaign = await _campaignService.GetCampaignByIdAsync(id);
            if (existingCampaign == null) return NotFound();

            await _campaignService.DeleteCampaignAsync(id);
            return NoContent();
        }
    }
}
