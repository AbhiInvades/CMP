using CMP_Server_API.Models.Clinic_Models;
using System.ComponentModel.DataAnnotations;

namespace CMP_Server_API.DTO
{
    public class ClinicDTO
    {
        [Required]
        [MinLength(6)]
        public string Name { get; set; }
        [Required]
        [MinLength(20)]
        public string Address { get; set; }
        public int Department { get; set; }

        public long Telephone { get; set; }
        public ClinicDTO() { }

        public ClinicDTO(string name, string address, int department, long telephone)
        {

            this.Name = name;
            this.Telephone = telephone;
            this.Address= address;
            this.Department = department;
        }


    }

    




}
