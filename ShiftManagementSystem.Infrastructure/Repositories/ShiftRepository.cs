using Microsoft.EntityFrameworkCore;
using ShiftManagementSystem.Domain.Entities;
using ShiftManagementSystem.Domain.Interfaces;
using ShiftManagementSystem.Infrastructure.Persistence;

namespace ShiftManagementSystem.Infrastructure.Repositories
{
    public class ShiftRepository : IShiftRepository
    {
        private readonly AppDbContext _context;

        public ShiftRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Shift>> GetAllAsync()
        {
            return await _context.Shifts
            .Include(s => s.ShiftAssignments)
               .ThenInclude(sa => sa.User)
            .ToListAsync();
        }

        public async Task<Shift?> GetByIdAsync(Guid id)
        {
            return await _context.Shifts
              .Include(s => s.ShiftAssignments)
              .ThenInclude(sa => sa.User)
              .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Shift>> GetShiftByDateAsync(DateTime date)
        {
            return await _context.Shifts 
            .Where(s => s.Date == date)
            .Include(s => s.ShiftAssignments)
            .ThenInclude(sa => sa.User)
            .ToListAsync();
        }

        public async Task<List<Shift>> GetShiftByUserIDAsync(Guid userId)
        {
             return await _context.Shifts
             .Where(s => s.ShiftAssignments.Any(sa => sa.UserId == userID))
             .Include(s => s.shiftAssignments)
             .ThenInclude(sa => sa.User)
             .ToListAsync();
        }


        public async Task AddAsync(Shift shift)
        {
            await _context.Shifts.AddAsync(shift);
            await _context.SaveChangesAsync();
            return shift;
        }

        public async Task UpdateAsync(Shift shift)
        {
            _context.Shifts.Update(shift);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var shift = await _context.Shifts.FindAsync(id);
            if (shift != null)
            {
                _context.Shifts.Remove(shift);
                await _context.SaveChangesAsync();
            }
        }
    }
}
