using CMP_Server_API.Models.Clinic_Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMP_Server_API.CMP.Data.Repositories.ClinicRepository
{
    public interface IClinicRepository
    {
        public abstract  Task<List<Clinic>> GetAll();
        public abstract Task<Clinic> Get(int id);
        public abstract Task<Clinic> Add(Clinic clinic);
        public abstract Task<Clinic> Delete(int id);
        public abstract Task<Clinic> Update(Clinic clinic);

    }
}
