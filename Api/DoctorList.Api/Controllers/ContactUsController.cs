using DoctorList.BL.DTOs;
using DoctorList.BL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoctorList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private readonly IContactUsService _contactUsService;

        public ContactUsController(IContactUsService contactUsService)
        {
            this._contactUsService = contactUsService;
        }

        // GET: api/<ContactUsController>
        [HttpPost("Send")]
        public ActionResult<bool> Send(ContactUsDto contactUsDto)
        {
            var result = _contactUsService.SaveContactUsData(contactUsDto);
            if(result == true)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
