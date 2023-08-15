using CMP_Server_API.Models.StaffModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace CMP_Server_API.CMP_Server_API.Data.Models.StaffModels
{
    public class StaffView
    {
        public int StaffID { get; set; }

        //instead of clinic id should have a Clinic class variable.
        [Required]
        [ForeignKey("Clinic")]
        public int ClinicID { get; set; }

        public string Name { get; set; }

        [Required]
        [EnumDataType(typeof(ShiftTime))]
        public ShiftTime ShiftTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public long Phone { get; set; }

        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        public Role Role { get; set; }

        public string fileName { get; set; }

        public StaffView(int staffID, int clinicID, string name, ShiftTime shiftTime, DateTime dob, int phone, Gender gender, Role role, string photo)
        {
            StaffID = StaffID;
            ClinicID = ClinicID;
            Name = name;
            Phone = phone;
            Gender = gender;
            Role = role;
            DOB= dob;
            ShiftTime= shiftTime;
            fileName = photo;
        }

        public StaffView() { }
    }
}
