using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShiftManagementSystem.Domain.Entities;
using ShiftManagementSystem.Domain.Interfaces;

namespace ShiftManagementSystem.Domain.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        // Specific methods for User
        Task<User> GetByEmailAsync(string email);
        Task<bool> EmailExistsAsync(string email);
    }
} 