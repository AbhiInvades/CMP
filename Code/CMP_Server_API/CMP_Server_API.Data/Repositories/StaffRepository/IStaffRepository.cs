using CMP_Server_API.CMP_Server_API.Data.Models.StaffModels;
using CMP_Server_API.Models.StaffModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMP_Server_API.CMP.Data.Repositories.StaffRepository
{
    public interface IStaffRepository
    {
        public abstract Task<List<StaffView>> GetAll(int id);
        public abstract Task<Staff> Add(Staff Staff);
        public abstract Task<Staff> Delete(int id);
        public abstract Task<int> Update(Staff Staff);
        public abstract Task<StaffView> Get(int id);

        public abstract Task<List<StaffView>> GetAllShift(ShiftTime shiftTime, int id);
        public abstract Task<Staff> GetPojo(int id);
    }
}
