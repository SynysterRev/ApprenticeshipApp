using Asp.Versioning;
using JuniorOnly.Application.DTO.Offer;
using JuniorOnly.Application.Interfaces;
using JuniorOnly.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace JuniorOnly.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class OffersController : CustomControllerBase
    {
        private readonly IOfferService _offerService;
        private readonly ILogger<OffersController> _logger;

        public OffersController(IOfferService offerService, ILogger<OffersController> logger)
        {
            _offerService = offerService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<OfferDto>>> GetAll()
        {
            var offers = await _offerService.GetAllOffersAsync();
            return Ok(offers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OfferDto>> GetById(Guid id)
        {
            var offer = await _offerService.GetOfferByIdAsync(id);
            return Ok(offer);
        }

        [HttpPost]
        public async Task<ActionResult<OfferDto>> Create(OfferCreateDto createDto)
        {
            var newOffer = await _offerService.CreateOfferAsync(createDto);
            _logger.LogInformation("Offer created with ID {offerID}", newOffer.Id);
            return CreatedAtAction(nameof(GetById), new { id = newOffer.Id }, newOffer);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OfferDto>> Update(Guid id, OfferUpdateDto updateDto)
        {
            var updatedOffer = await _offerService.UpdateOfferAsync(id, updateDto);
            _logger.LogInformation("Offer updated with ID {offerID}", id);
            return Ok(updatedOffer);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _offerService.DeleteOfferAsync(id);
            _logger.LogInformation("Offer deleted with ID {offerID}", id);
            return NoContent();
        }
    }
}
