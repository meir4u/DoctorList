using DoctorList.BL.DTOs;
using DoctorList.BL.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DoctorList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            this._doctorService = doctorService;
        }

        // GET: api/<DoctorController>
        [OutputCache(Duration = 60)] //1 minut
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> Get()
        {
            var result = await _doctorService.GetAllDoctors();
            return Ok(result);
        }
    }
}
