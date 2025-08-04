using Asp.Versioning;
using JuniorOnly.Application.DTO.Application;
using JuniorOnly.Application.DTO.Offer;
using JuniorOnly.Application.DTO.Pagination;
using JuniorOnly.Application.Interfaces;
using JuniorOnly.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace JuniorOnly.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class OffersController : CustomControllerBase
    {
        private readonly IOfferService _offerService;
        private readonly IApplicationService _applicationService;
        private readonly ILogger<OffersController> _logger;

        public OffersController(IOfferService offerService, ILogger<OffersController> logger, IApplicationService applicationService)
        {
            _offerService = offerService;
            _logger = logger;
            _applicationService = applicationService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<OfferDto>>> GetAll([FromQuery] Guid? companyId, [FromQuery] int pageNumber)
        {
            if (companyId.HasValue)
            {
                var offers = await _offerService.GetOffersByCompanyAsync(companyId.Value, pageNumber);
                return Ok(offers);
            }
            else
            {
                var offers = await _offerService.GetAllOffersAsync(pageNumber);
                return Ok(offers);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OfferDto>> GetById(Guid id)
        {
            var offer = await _offerService.GetOfferByIdAsync(id);
            return Ok(offer);
        }

        [HttpGet("{offerId}/applications")]
        public async Task<ActionResult<List<ApplicationDto>>> GetApplications(Guid offerId)
        {
            var applications = await _applicationService.GetApplicationsByOfferAsync(offerId);
            return Ok(applications);
        }

        [HttpGet("search")]
        public async Task<ActionResult<PaginatedResponse<OfferDto>>> Search([FromQuery] OfferSearchQuery query, [FromQuery] int pageNumber)
        {
            var offers = await _offerService.SearchOffersAsync(query, pageNumber);
            return Ok(offers);
        }

        [HttpGet("count")]
        public async Task<ActionResult<int>> GetOffersCount()
        {
            int total = await _offerService.GetOffersCountAsync();
            return Ok(total);
        }

        [HttpGet("latest")]
        public async Task<ActionResult<List<OfferDto>>> GetLastestOffers([FromQuery] int count)
        {
            var offers = await _offerService.GetLastestOffersAsync(count);
            return Ok(offers);
        }

        [HttpPost]
        public async Task<ActionResult<OfferDto>> Create(OfferCreateDto createDto)
        {
            var newOffer = await _offerService.CreateOfferAsync(createDto);
            _logger.LogInformation("Offer created with ID {offerID}", newOffer.Id);
            return CreatedAtAction(nameof(GetById), new { id = newOffer.Id }, newOffer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OfferDto>> Update(Guid id, [FromBody] OfferUpdateDto updateDto)
        {
            var updatedOffer = await _offerService.UpdateOfferAsync(id, updateDto);
            _logger.LogInformation("Offer updated with ID {offerID}", id);
            return Ok(updatedOffer);
        }

        // Avoid to use it accidently
        //[HttpDelete("{id}/permanent")]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    await _offerService.DeleteOfferAsync(id);
        //    _logger.LogInformation("Offer deleted with ID {offerID}", id);
        //    return NoContent();
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(Guid id)
        {
            await _offerService.SoftDeleteOfferAsync(id);
            _logger.LogInformation("Offer soft deleted with ID {offerID}", id);
            return NoContent();
        }
    }
}
