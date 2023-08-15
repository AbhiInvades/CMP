using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMP_Server_API.Models.Clinic_Models
{
    public class Clinic
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ClinicId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [EnumDataType(typeof(Department))]
        public Department Department { get; set; }

        public long Telephone { get; set; }

        //should contain a list of staff

        public Clinic(string name,string address, Department department,long telephone) 
        {
            this.Name = name;
            this.Address = address;
            this.Department = department;
            this.Telephone = telephone;
        }

        public Clinic() { }
        
    }

    public enum Department
    {
        Cardiology,Dermatology,Neorology,Psychology
    }

}
