using ShiftManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShiftManagementSystem.Domain.Interfaces
{
    public interface IShiftAssignmentRepository : IBaseRepository<ShiftAssignment>
    {
        // Get all assignments for a specific shift
        Task<List<ShiftAssignment>> GetByShiftIdAsync(Guid shiftId);
        
        // Get all assignments for a specific user
        Task<List<ShiftAssignment>> GetByUserIdAsync(Guid userId);
        
        // Check if a user is already assigned to a shift
        Task<bool> IsUserAssignedToShiftAsync(Guid userId, Guid shiftId);
        
        // Delete a specific assignment
        Task DeleteAssignmentAsync(Guid userId, Guid shiftId);
    }
}