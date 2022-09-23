using MedGrupo.Api.DTO;
using MedGrupo.Domain.ContactAggregate;
using MedGrupo.Domain.ContactAggregate.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace MedGrupo.Api
{
    [Route("[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactServices contactServices;

        public ContactController(IContactServices contactServices)
        {
            this.contactServices = contactServices;
        }
        [HttpGet("{id:int}", Name = "Get Contact")]
        public IActionResult Get(int id)
        {
            try
            {
                var response = new ApiResponse<Contact>(contactServices.GetContact(id));
                return Ok(response);
            }
            catch (ContactException ex)
            {
                return BadRequest(new ApiResponse<object>(new string[] {ex.Message}));
            }
            catch (System.Exception)
            {
                return BadRequest(new ApiResponse<string>("ERROR: Internal server error."));
            }
        }
        [HttpGet("", Name = "Get Contacts.")]
        public IActionResult GetList()
        {
            try
            {
                var response = new ApiResponse<IEnumerable<Contact>>(contactServices.GetContacts());
                return Ok(response);
            }
            catch (ContactException ex)
            {
                return BadRequest(new ApiResponse<object>(new string[] {ex.Message}));
            }
            catch (System.Exception)
            {
                return BadRequest(new ApiResponse<string>("ERROR: Internal server error."));
            }
        }
        [HttpPost("", Name = "Create Contact.")]
        public IActionResult Post([FromBody] CreateContactDTO contactDTO)
        {
            try
            {
                var response = new ApiResponse<Contact>(contactServices.CreateContact(contactDTO.ToDomain()));
                return Ok(response);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ApiResponse<object>(ex.InnerExceptions.Select(e => e.Message)));
            }
            catch (System.Exception)
            {
                return BadRequest(new ApiResponse<string>("ERROR: Internal server error."));
            }
        }
        [HttpPut("{id:int}", Name = "Update contact")]
        [Consumes("application/json")]
        public IActionResult Put(int id, [FromBody] UpdateContactDTO contactDTO)
        {
            try
            {
                var response = contactServices.UpdateContact(id, contactDTO.ToDomain());
                return Ok(new ApiResponse<Contact>(response));
            }
            catch (ValidationException exs)
            {
                return BadRequest(new ApiResponse<string>(exs.InnerExceptions.Select(e => e.Message)));
            }
            catch (ContactException ex)
            {
                return BadRequest(new ApiResponse<object>(new string[] {ex.Message}));
            }
            catch (System.Exception)
            {
                return BadRequest(new ApiResponse<string>("ERROR: Internal server error."));
            }
        }
        [HttpDelete("{id:int}", Name = "Delete contact")]
        public IActionResult Delete(int id)
        {
            try
            {
                contactServices.DeleteContact(id);
                return NoContent();
            }
            catch (ContactException ex)
            {
                return BadRequest(new ApiResponse<object>(new string[] {ex.Message}));
            }
            catch (System.Exception)
            {
                return BadRequest(new ApiResponse<string>("ERROR: Internal server error."));
            }
        }
    }
}