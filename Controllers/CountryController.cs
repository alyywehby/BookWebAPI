using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using webapi.DTOS;
using webapi.Models;
using webapi.Services;
namespace webapi.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class CountryController : Controller {
        private ICountryRepository _countryRepository;

        public CountryController (ICountryRepository countryRepository) {
            _countryRepository = countryRepository;
        }

        //api/country
        [HttpGet]
        [ProducesResponseType (400)]
        [ProducesResponseType (200, Type = typeof (IEnumerable<CountryDTO>))]
        public IActionResult GetCountries () {
            var countries = _countryRepository.GetCountries ().ToList ();
            if (!ModelState.IsValid)
                return BadRequest (ModelState);
            var countriesDTO = new List<CountryDTO> ();
            foreach (var country in countries) {
                countriesDTO.Add (new CountryDTO { Id = country.Id, Name = country.Name });
            }
            return Ok (countriesDTO);
        }

        //api/country/countryId
        [HttpGet ("{countryId}", Name="GetCountry")]
        [ProducesResponseType (400)]
        [ProducesResponseType (404)]
        [ProducesResponseType (200, Type = typeof (CountryDTO))]
        public IActionResult GetCountry (int countryId) {
            if (!_countryRepository.CountryExists (countryId))
                return NotFound ();
            var country = _countryRepository.GetCountry (countryId);
            if (!ModelState.IsValid)
                return BadRequest (ModelState);
            var countryDTO = new CountryDTO () { Id = country.Id, Name = country.Name };
            return Ok (countryDTO);
        }

        //api/country/author/authorId
        [HttpGet ("author/{authorId}")]
        [ProducesResponseType (400)]
        [ProducesResponseType (404)]
        [ProducesResponseType (200, Type = typeof (CountryDTO))]
        public IActionResult GetCountryOfAnAuthor (int authorId) {
            var country = _countryRepository.GetCountryOfAuthor (authorId);
            if (!ModelState.IsValid)
                return BadRequest (ModelState);
            var countryDTO = new CountryDTO () { Id = country.Id, Name = country.Name };
            return Ok (countryDTO);
        }

        //api/country/countryId/authors
        [HttpGet ("{countryId}/authors")]
        [ProducesResponseType (400)]
        [ProducesResponseType (404)]
        [ProducesResponseType (200, Type = typeof (IEnumerable<AuthorDTO>))]
        public IActionResult GetAuthorsOfACountry (int countryId) {
            var authors = _countryRepository.GetAuthorsFromACountry (countryId).ToList ();
            if (!ModelState.IsValid)
                return BadRequest (ModelState);
            var authorsDTO = new List<AuthorDTO> ();
            foreach (var author in authors) {
                authorsDTO.Add (new AuthorDTO () {
                    Id = author.Id,
                        FirstName = author.FirstName,
                        LastName = author.LastName
                });
            }
            return Ok (authorsDTO);
        }

        //api/country
        [HttpPost]
        [ProducesResponseType (201, Type = typeof (Country))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult CreateCountry ([FromBody] Country country) {
            if (country == null || !ModelState.IsValid)
                return BadRequest (ModelState);
            if (_countryRepository.IsDuplicateCountryName (country.Id, country.Name)) {
                ModelState.AddModelError ("", $"Country {country.Name} already exists");
                return StatusCode (422, $"Country {country.Name} already exists");
            }
            if (!_countryRepository.CreateCountry (country)) {
                ModelState.AddModelError ("", $"Something went wrong savig {country.Name}");
                return StatusCode (500, ModelState);
            }
            return CreatedAtRoute ("GetCountry", new { countryId = country.Id }, country);
        }
    }
}