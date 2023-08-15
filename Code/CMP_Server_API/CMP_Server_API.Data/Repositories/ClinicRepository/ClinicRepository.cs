using CMP_Server_API.Models.Clinic_Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMP_Server_API.CMP.Data.Repositories.ClinicRepository
{
    public class ClinicRepository : IClinicRepository
    {
        private readonly ClinicDBContext _context;
        public ClinicRepository() { }
        public ClinicRepository(ClinicDBContext dbContext) 
        {
            _context= dbContext;
        }
        public async Task<Clinic> Add(Clinic clinic)
        {
            _context.Add(clinic);
            await _context.SaveChangesAsync();
            return clinic;
        }

        public async Task<Clinic> Delete(int id)
        {
            var clinic = await Get(id);
            _context.Clinic.Remove(clinic);
            await _context.SaveChangesAsync();
            return clinic;
        }      
               
        public async Task<Clinic> Get(int id)
        {
            return await _context.Clinic.FindAsync(id);
        }

        public async Task<List<Clinic>> GetAll()
        {
            return await _context.Clinic.ToListAsync();
        }

        public async Task<Clinic> Update(Clinic clinic)
        {
            _context.Clinic.Update(clinic);
            await _context.SaveChangesAsync();
            return clinic;
        }
    }
}
