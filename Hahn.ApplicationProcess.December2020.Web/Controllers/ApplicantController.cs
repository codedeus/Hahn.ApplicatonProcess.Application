using AutoMapper;
using Hahn.ApplicationProcess.December2020.Domain.Models;
using Hahn.ApplicationProcess.December2020.Domain.Repository;
using Hahn.ApplicationProcess.December2020.Web.Attributes;
using Hahn.ApplicationProcess.December2020.Web.Models.Request;
using Hahn.ApplicationProcess.December2020.Web.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.December2020.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModel]
    [Produces("application/json")]
    public class ApplicantController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IApplicantRepository _repository;
        public ApplicantController( IMapper mapper, IApplicantRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        /// <summary>
        /// Returns list of registered applicants
        /// </summary>
        /// <returns>Applicant with the given ID</returns>
        /// <response code="200">Applicant List</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<ApplicantResponse>>> Get()
        {
            //return Ok();
            var items = (await _repository.ListAsync<Applicant>())
                .Select(item => _mapper.Map<Applicant, ApplicantResponse>(item));
            return Ok(items);
        }

        /// <summary>
        /// Returns an Applicant Object.
        /// </summary>
        /// <param name="id" example="1"></param>
        /// <returns>Applicant with the given ID</returns>
        /// <response code="200">Applicant Object</response>
        /// <response code="400">If no applicant with the given ID is found</response>      
        /// <response code="404">ID not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(500)]
        [ProducesResponseType(typeof(ValidateModelAttribute.ValidationResultModel), 400)]
        public async Task<ActionResult<ApplicantResponse>> Get(int id)
        {
            var item = await _repository.GetByIdAsync<Applicant>(id);
            if (item == null)
            {
                ModelState.AddModelError("id", $"id:{id} not found");
                return BadRequest(new ValidateModelAttribute.ValidationResultModel(ModelState));
            }
            return _mapper.Map<Applicant, ApplicantResponse>(item);
        }


        /// <summary>
        /// Saves a new applicant details
        /// </summary>
        /// <returns>The newly created applicant object</returns>
        /// <response code="201">Applicant Object</response>
        /// <response code="400">invalid object or parameter</response>
        /// <response code="500">something went wrong</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidateModelAttribute.ValidationResultModel), 400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ApplicantResponse>> Post([FromBody] ApplicantRequest applicantRequest)
        {
            var applicantToCreate = _mapper.Map<ApplicantRequest, Applicant>(applicantRequest);

            var applicantExist = await _repository.GetApplicantByEmail(applicantToCreate.EmailAddress);
            if (applicantExist != null)
            {
                ModelState.AddModelError("emailAddress", $"Applicant with emaill address:{applicantExist.EmailAddress} already exists");
                return BadRequest(new ValidateModelAttribute.ValidationResultModel(ModelState));
            }

            var savedApplicant = await _repository.AddAsync(applicantToCreate);
            return CreatedAtAction(nameof(Get), new { id = savedApplicant.ID }, _mapper.Map<Applicant, ApplicantResponse>(savedApplicant));
        }


        /// <summary>
        /// updates the applicant details with the given update applicant object
        /// </summary>
        /// <param name="id" example="1"></param>
        /// <param name="applicant"></param>
        /// <returns>The updated applicant object</returns>
        /// <response code="200">Applicant Object</response>
        /// <response code="400">invalid object or parameter</response>
        /// <response code="500">something went wrong</response>
        [HttpPut("{id}")]
        [ValidateModel]
        [ProducesResponseType(typeof(ValidateModelAttribute.ValidationResultModel), 400)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ApplicantResponse>> Put(int id, [FromBody] ApplicantRequest applicant)
        {
            var existingItem = await _repository.GetByIdAsync<Applicant>(id);
            if (existingItem == null)
            {
                ModelState.AddModelError("id", $"id:{id} not found");
                return BadRequest(new ValidateModelAttribute.ValidationResultModel(ModelState));
            }
            existingItem.Age = applicant.Age;
            existingItem.CountryOfOrigin = applicant.CountryOfOrigin;
            existingItem.EmailAddress = applicant.EmailAddress;
            existingItem.FamilyName = applicant.FamilyName;
            existingItem.Hired = applicant.Hired;
            existingItem.Name = applicant.Name;
            existingItem.Address = applicant.Address;

            await _repository.UpdateAsync(existingItem);
            return Ok(_mapper.Map<Applicant, ApplicantResponse>(existingItem));
        }


        /// <summary>
        /// deletes the applicant with the given ID
        /// </summary>
        /// <param name="id" example="1"></param>
        /// <response code="400">invalid object or parameter</response>
        /// <response code="500">something went wrong</response>
        /// <response code="204"></response>
        /// <response code="500">something went wrong</response>
        // DELETE api/<ApplicationController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidateModelAttribute.ValidationResultModel), 400)]
        public async Task<ActionResult<ApplicantResponse>> Delete(int id)
        {
            var itemToDelete = await _repository.GetByIdAsync<Applicant>(id);
            if (itemToDelete == null)
            {
                ModelState.AddModelError("id", $"id:{id} not found");
                return BadRequest(new ValidateModelAttribute.ValidationResultModel(ModelState));
            }
            await _repository.DeleteAsync(itemToDelete);

            return NoContent();
        }
    }
}
