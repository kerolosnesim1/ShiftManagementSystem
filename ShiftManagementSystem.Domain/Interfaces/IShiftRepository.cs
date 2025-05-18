using ShiftManagementSystem.Domain.Entities;

namespace ShiftManagementSystem.Domain.Interfaces
{
    public interface IShiftRepository
    {
        Task<List<Shift>> GetAllAsync();
        Task<Shift> GetByIdAsync(Guid id);
        Task<List<Shift>> GetShiftsByDateAsync(DateTime date);
        Task<List<Shift>> GetShiftsByUserIdAsync(Guid userId);
        Task AddAsync(Shift shift);
        Task UpdateAsync(Shift shift);
        Task DeleteAsync(Guid id);
    }
}
