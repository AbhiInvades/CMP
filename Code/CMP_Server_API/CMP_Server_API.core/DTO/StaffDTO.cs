using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System;
using CMP_Server_API.Models.StaffModels;
using CMP_Server_API.Models.StaffModels;

namespace CMP_Server_API.DTO
{
    public class StaffDTO
    { 
        public int StaffID { get; set; }

        //[Required]
        //[ForeignKey("Clinic")]
        [Required]
        public int ClinicID { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        [EnumDataType(typeof(ShiftTime))]
        public ShiftTime ShiftTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        public long Phone { get; set; }

        [EnumDataType(typeof(Gender))]
        public int Gender { get; set; }

        [EnumDataType(typeof(Role))]
        public Role Role { get; set; }

        public IFormFile Photo { get; set; }

        public string PhotoString { get; set; }

     
        public string FileName { get; set; }

        public string Password { get; set; }

        public StaffDTO()
        {

        }

        public StaffDTO (int staffID, int clinicID, string name, int shiftTime, DateTime dOB, long phone, int gender, int role, IFormFile photo, string photoString, string password, string fileName)
        {
            this.StaffID = staffID;
            this.ClinicID = clinicID;
            this.Name = name;
            this.ShiftTime = (ShiftTime) shiftTime;
            this.DOB = dOB;
            this.Phone = phone;
            this.Gender = gender;
            this.Role = (Role)role;
            this.Photo = photo;
            this.PhotoString = photoString;
            this.Password = password;
            this.FileName = fileName;
        }
    }
}
