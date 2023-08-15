using CMP_Server_API.Models.Clinic_Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CMP_Server_API.Models.StaffModels
{

    public class Staff
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StaffID { get; set; }

        //instead of clinic id should have a Clinic class variable.
        [Required]
        public int ClinicID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EnumDataType(typeof(ShiftTime))]
        public ShiftTime ShiftTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [DataType(DataType.PhoneNumber)]
        public long Phone { get; set; }

        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        [EnumDataType(typeof(Role))]
        public Role Role { get; set; }

        public string FileName { get; set; }

        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$")]
        public string Password { get; set; }

        public Clinic Clinic { get; set; }

        public Staff(int staffID, int clinicID, string name, ShiftTime shiftTime, DateTime dob, long phone, Gender gender, Role role, string photo, string password)
        {
            Password= password;
            StaffID = staffID;
            ClinicID = clinicID;
            Name = name;
            ShiftTime = shiftTime;
            DOB = dob;
            Phone = phone;
            Gender = gender;
            Role = role;
            FileName = photo;
        }

        public Staff() { }

    }

    //[JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ShiftTime
    {
        //  [EnumMember]
        MORNING = 0,
        //[EnumMember]
        EVENING = 1,
        //[EnumMember]
        NIGHT = 2
    }

    public enum Gender
    {
        MALE = 0,
        FEMALE = 1,
        OTHER = 2
    }

    public enum Role
    {
        Admin, Nurse,Doctor,Receptionist
    }
}
