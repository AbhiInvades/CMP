using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMP_Server_API.CMP.Data;
using CMP_Server_API.DTO;
using CMP_Server_API.Models.Clinic_Models;
using CMP_Server_API.CMP.Data.Repositories.ClinicRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace CMP_Server_API.Controllers.Clinic_Management
{
    [ApiController]
    [Route("clinic")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClinicController : ControllerBase
    {
        private readonly IClinicRepository _repository;

        public ClinicController(IClinicRepository repository)
        {
            System.Threading.Thread.Sleep(1000);
            _repository = repository;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            
            List<Clinic> list = await _repository.GetAll();
            return Ok(list);
        }

        [HttpGet("details/{id:int}")]
        public async Task<IActionResult> Details([FromRoute] int id)
        {

            var clinic = await _repository.Get(id);
            if (clinic == null)
            {
                return NotFound();
            }

            return Ok(clinic);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ClinicDTO clinic)
        {
            Console.WriteLine(clinic);
            if (ModelState.IsValid)
            {
                Clinic clinic3 = new Clinic(clinic.Name, clinic.Address, (Department)clinic.Department, clinic.Telephone);
                await _repository.Add(clinic3);
                return Ok(clinic3);
            }
            return BadRequest();
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteConfirmed([FromRoute]int id)
        {
            var clinic = await _repository.Delete(id);
            return Ok(clinic);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(Clinic clinic)
        {
            Clinic clinic1 = null;
            if(clinic == null)
            {
                return NotFound();
            }
            else
            {
                clinic1 = await _repository.Update(clinic);
            }
            return Ok(clinic1);
        }

    }
}
