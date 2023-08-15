using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CMP_Server_API.DTO;
using CMP_Server_API.Models.StaffModels;
using CMP_Server_API.CMP_Server_API.Infra.Services.Utility;
using CMP_Server_API.CMP.Data.Repositories.StaffRepository;
using CMP_Server_API.Models.Clinic_Models;
using CMP_Server_API.CMP_Server_API.Data.Models.StaffModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http;
using System.Diagnostics;

namespace CMP_Server_API.CMP_Server_API.core.Controllers.Clinic_Management
{
    [ApiController]
    [Route("staff")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StaffController : ControllerBase
    {
        private readonly IFileHandling _fileHandler;
        private readonly IStaffRepository _repository;
        private readonly IEncryptor _encryptor;

        public StaffController(IFileHandling fileHandler, IStaffRepository repsository, IEncryptor encryptor)
        {
            System.Threading.Thread.Sleep(1000);
            _fileHandler = fileHandler;
            _repository = repsository;
            _encryptor = encryptor;
            Debug.WriteLine("was i here");
            Console.WriteLine("yes indeed");
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Index([FromRoute] int id)
        {
            Console.WriteLine(id);
            var list = await _repository.GetAll(id);
            return Ok(list);
        }

        [HttpGet("shift")]
        public async Task<IActionResult> GetAllShift([FromRoute] ShiftTime shiftTime, [FromQuery]int id)
        {
            if(shiftTime != null && id != null)
            {
                return Ok(_repository.GetAllShift(shiftTime, id));
            }
            else
            {
                return Ok(null);
            }

        }

        [HttpGet("details/{id:int}")]
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var staff = await _repository.Get(id);
            if (staff == null)
            {
                return NotFound();
            }
            string path = staff.fileName;
            StaffDTO dto = new StaffDTO(staff.StaffID, staff.ClinicID, staff.Name, (int)staff.ShiftTime, staff.DOB, staff.Phone, (int)staff.Gender, (int)staff.Role, null, null, null, staff.fileName);

            return Ok(dto);
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] StaffDTO staffDTO)
        {
            if (ModelState.IsValid)
            {
                ShiftTime shift = (ShiftTime)Enum.ToObject(typeof(ShiftTime), staffDTO.ShiftTime);
                String fileName = _fileHandler.storeFile(staffDTO.PhotoString, staffDTO.FileName);
                String EncryptedPWD = _encryptor.encrypt(staffDTO.Password);
                Gender gender = (Gender)staffDTO.Gender;
                Staff staff = new Staff(0, staffDTO.ClinicID, staffDTO.Name, shift, staffDTO.DOB, staffDTO.Phone, gender, staffDTO.Role, fileName, EncryptedPWD);
                await _repository.Add(staff);
                staffDTO.Password = null;
                staffDTO.Photo = null;
                return Ok(staffDTO);
            }
            return BadRequest();
        }

        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> DeleteConfirmed([FromRoute]int id)
        {
            StaffView staff = await _repository.Get(id);
            _fileHandler.deleteFile(staff.fileName);
            await _repository.Delete(id);
            return Ok(id);
        }

        [HttpPut("update")]
        public async Task<IActionResult> update([FromBody] StaffDTO staffDTO)
        {
            

            if (ModelState.IsValid)
            {
                string fileName, EncryptedPWD;
                ShiftTime shift = (ShiftTime)Enum.ToObject(typeof(ShiftTime), staffDTO.ShiftTime);
                Gender gender = (Gender)staffDTO.Gender;

                if (staffDTO.FileName == null)
                {
                    Staff staffT = await _repository.GetPojo(staffDTO.ClinicID);
                    fileName = staffT.FileName;
                }else
                {
                    fileName = _fileHandler.storeFile(staffDTO.PhotoString, staffDTO.FileName);
                }

                
                if(staffDTO.Password == null)
                {
                    Staff staffT = await _repository.GetPojo(staffDTO.ClinicID);
                    EncryptedPWD = staffT.Password;
                }else
                {
                    EncryptedPWD = _encryptor.encrypt(staffDTO.Password);
                }

                Staff staff = new Staff(staffDTO.StaffID, staffDTO.ClinicID, staffDTO.Name, shift, staffDTO.DOB, staffDTO.Phone, gender, staffDTO.Role, fileName, EncryptedPWD);
                
                var clinicChanged = await _repository.Update(staff);
                return Ok(clinicChanged);
            }
            return BadRequest();
        }

        [HttpPut("shift")]
        public async Task<ActionResult> updateShift([FromBody] StaffDTO staffDTO)
        {
            if (ModelState.IsValid)
            {
                ShiftTime shift = (ShiftTime)Enum.ToObject(typeof(ShiftTime), staffDTO.ShiftTime);
                Staff staff = await _repository.GetPojo(staffDTO.StaffID);
                staff.ShiftTime = shift;
                var clinicChanged = await _repository.Update(staff);
                return Ok(clinicChanged);
            }
            return BadRequest();
        }

    }
}
