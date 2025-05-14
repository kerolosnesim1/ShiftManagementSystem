using Microsoft.EntityFrameworkCore;
using ShiftManagementSystem.Domain.Entities;
using ShiftManagementSystem.Domain.Interfaces;
using ShiftManagementSystem.Infrastructure.Persistence;

namespace ShiftManagementSystem.Infrastructure.Repositories
{
    public class ShiftAssignmentRepository : IShiftAssignmentRepository
    {
        private readonly AppDbContext _context;

        public ShiftAssignmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ShiftAssignment>> GetAllAsync()
        {
            return await _context.ShiftAssignments
                .Include(sa => sa.User)
                .Include(sa => sa.Shift)
                .ToListAsync();
        }

        public async Task<ShiftAssignment> GetByIdAsync(Guid id)
        {
            return await _context.ShiftAssignments
                .Include(sa => sa.User)
                .Include(sa => sa.Shift)
                .FirstOrDefaultAsync(sa => sa.Id == id);
        }

        public async Task<List<ShiftAssignment>> GetByShiftIdAsync(Guid shiftId)
        {
            return await _context.ShiftAssignments
                .Include(sa => sa.User)
                .Where(sa => sa.ShiftId == shiftId)
                .ToListAsync();
        }

        public async Task<List<ShiftAssignment>> GetByUserIdAsync(Guid userId)
        {
            return await _context.ShiftAssignments
                .Include(sa => sa.Shift)
                .Where(sa => sa.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool> IsUserAssignedToShiftAsync(Guid userId, Guid shiftId)
        {
            return await _context.ShiftAssignments
                .AnyAsync(sa => sa.UserId == userId && sa.ShiftId == shiftId);
        }

        public async Task<ShiftAssignment> AddAsync(ShiftAssignment assignment)
        {
            await _context.ShiftAssignments.AddAsync(assignment);
            await _context.SaveChangesAsync();
            return assignment;
        }

        public async Task UpdateAsync(ShiftAssignment assignment)
        {
            _context.ShiftAssignments.Update(assignment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var assignment = await _context.ShiftAssignments.FindAsync(id);
            if (assignment != null)
            {
                _context.ShiftAssignments.Remove(assignment);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAssignmentAsync(Guid userId, Guid shiftId)
        {
            var assignment = await _context.ShiftAssignments
                .FirstOrDefaultAsync(sa => sa.UserId == userId && sa.ShiftId == shiftId);
            
            if (assignment != null)
            {
                _context.ShiftAssignments.Remove(assignment);
                await _context.SaveChangesAsync();
            }
        }
    }
}