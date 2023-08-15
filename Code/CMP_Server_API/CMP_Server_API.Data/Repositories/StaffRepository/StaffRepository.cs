using CMP_Server_API.Models.Clinic_Models;
using CMP_Server_API.Models.StaffModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using CMP_Server_API.CMP_Server_API.Data.Models.StaffModels;
using System;

namespace CMP_Server_API.CMP.Data.Repositories.StaffRepository
{
    public class StaffRepository : IStaffRepository
    {
        private readonly ClinicDBContext _context;
        public StaffRepository() { }
        public StaffRepository(ClinicDBContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<Staff> Add(Staff Staff)
        {
            _context.Add(Staff);
            await _context.SaveChangesAsync();
            return Staff;
        }

        public async Task<Staff> Delete(int id)
        {
            var Staff = _context.Staff.FirstOrDefault(predicate: x => x.StaffID == id);
            _context.Staff.Remove(Staff);
            await _context.SaveChangesAsync();
            return Staff;
        }

        public async Task<StaffView> Get(int id)
        {
            List<StaffView> staff = await _context.StaffView.ToListAsync();
            return staff.FirstOrDefault(x => x.StaffID == id);
        }

        public async Task<List<StaffView>> GetAll(int id)
        {
            return await _context.StaffView.Where(x => x.ClinicID == id).ToListAsync();
        }

        public async Task<int> Update(Staff staff)
        {
            _context.Staff.Update(staff);
            var i = await _context.SaveChangesAsync();
            try
            {
               // _context.Staff.Update(Staff);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, ex.StackTrace);
            }
            
            return i;
        }

        public async Task<List<StaffView>> GetAllShift(ShiftTime shiftTime, int id)
        {
            List<StaffView> list = await GetAll(id);
            return list.Where(x => x.ShiftTime == shiftTime).ToList();
        }

        public async Task<Staff> GetPojo(int id)
        {
            return await _context.Staff.FirstOrDefaultAsync(x => x.StaffID == id);
        }

    }
}
