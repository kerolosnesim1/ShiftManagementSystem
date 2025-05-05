using ShiftManagementSystem.Domain.Entities;

namespace ShiftManagementSystem.Domain.Interfaces
{
    public interface IShiftRepository
    {
        Task<List<Shift>> GetAllAsync();
        Task<Shift?> GetByIdAsync(Guid id);
        Task AddAsync(Shift shift);
        Task UpdateAsync(Shift shift);
        Task DeleteAsync(Guid id);
    }
}
