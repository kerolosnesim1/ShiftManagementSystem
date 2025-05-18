using ShiftManagementSystem.Application.DTOs.Shifts;
using ShiftManagementSystem.Application.DTOs.Users;
using ShiftManagementSystem.Domain.Entities;
using ShiftManagementSystem.Domain.Interfaces;

namespace ShiftManagementSystem.Application.Services.Shifts;

public class ShiftService
{
    
    private readonly IShiftRepository _shiftRepository;
    private readonly IShiftAssignmentRepository _assignmentRepository;
    private readonly IUserRepository _userRepository;

    // Constructor - gets database access through dependency injection
    public ShiftService(
        IShiftRepository shiftRepository,
        IShiftAssignmentRepository assignmentRepository,
        IUserRepository userRepository)
    {
        _shiftRepository = shiftRepository;
        _assignmentRepository = assignmentRepository;
        _userRepository = userRepository;
    }

    // Get all shifts
    public async Task<List<ShiftDto>> GetAllShiftsAsync()
    {
        
        var shifts = await _shiftRepository.GetAllAsync();
        
        
        var result = new List<ShiftDto>();
        foreach (var shift in shifts)
        {
            var shiftDto = await ConvertShiftToDto(shift);
            result.Add(shiftDto);
        }
        
        return result;
    }

    // Get a specific shift by ID
    public async Task<ShiftDto?> GetShiftByIdAsync(Guid id)
    {
       
        var shift = await _shiftRepository.GetByIdAsync(id);
        
       
        if (shift == null)
            return null;

        
        return await ConvertShiftToDto(shift);
    }

    // Get shifts on a specific date
    public async Task<List<ShiftDto>> GetShiftsByDateAsync(DateTime date)
    {
        
        var shifts = await _shiftRepository.GetShiftsByDateAsync(date);
        
       
        var result = new List<ShiftDto>();
        foreach (var shift in shifts)
        {
            result.Add(await ConvertShiftToDto(shift));
        }
        
        return result;
    }

    // Get shifts assigned to a specific user
    public async Task<List<ShiftDto>> GetShiftsByUserIdAsync(Guid userId)
    {
       
        var shifts = await _shiftRepository.GetShiftsByUserIdAsync(userId);
        
        
        var result = new List<ShiftDto>();
        foreach (var shift in shifts)
        {
            result.Add(await ConvertShiftToDto(shift));
        }
        
        return result;
    }

    // Create a new shift
    public async Task<ShiftDto> CreateShiftAsync(CreateShiftDto model)
    {
        
        if (model.StartTime >= model.EndTime)
        {
            throw new ArgumentException("Shift cannot end before it starts!");
        }

        
        var shift = new Shift
        {
            Date = model.Date,
            StartTime = model.StartTime,
            EndTime = model.EndTime,
            ShiftType = model.ShiftType
        };

        
        await _shiftRepository.AddAsync(shift);

        
        return await ConvertShiftToDto(shift);
    }

    // Update an existing shift
    public async Task<bool> UpdateShiftAsync(Guid id, UpdateShiftDto model)
    {
        
        if (model.StartTime >= model.EndTime)
        {
            throw new ArgumentException("Shift cannot end before it starts!");
        }

        
        var shift = await _shiftRepository.GetByIdAsync(id);
        if (shift == null)
            return false;  

        
        shift.Date = model.Date;
        shift.StartTime = model.StartTime;
        shift.EndTime = model.EndTime;
        shift.ShiftType = model.ShiftType;

      
        await _shiftRepository.UpdateAsync(shift);
        return true;  
    }

    // Delete a shift
    public async Task<bool> DeleteShiftAsync(Guid id)
    {
        
        var shift = await _shiftRepository.GetByIdAsync(id);
        if (shift == null)
            return false;  

        
        await _shiftRepository.DeleteAsync(id);
        return true;  
    }

    // Assign a user to work a shift
    public async Task<bool> AssignUserToShiftAsync(Guid shiftId, Guid userId)
    {
       
        var shift = await _shiftRepository.GetByIdAsync(shiftId);
        if (shift == null)
            return false;  

        
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            return false;  

       
        var isAlreadyAssigned = await _assignmentRepository.IsUserAssignedToShiftAsync(userId, shiftId);
        if (isAlreadyAssigned)
            return false; 

        
        var assignment = new ShiftAssignment
        {
            UserId = userId,
            ShiftId = shiftId
        };

        
        await _assignmentRepository.AddAsync(assignment);
        return true;  
    }

    // Remove a user from a shift
    public async Task<bool> RemoveUserFromShiftAsync(Guid shiftId, Guid userId)
    {
       
        var isAssigned = await _assignmentRepository.IsUserAssignedToShiftAsync(userId, shiftId);
        if (!isAssigned)
            return false; 

       
        await _assignmentRepository.DeleteAssignmentAsync(userId, shiftId);
        return true;  
    }

   
    private async Task<ShiftDto> ConvertShiftToDto(Shift shift)
    {
        
        var assignments = await _assignmentRepository.GetByShiftIdAsync(shift.ID);
        
        
        var assignedUsers = new List<UserBasicDto>();
        
        
        foreach (var assignment in assignments)
        {
            var user = await _userRepository.GetByIdAsync(assignment.UserId);
            if (user != null)
            {
               
                assignedUsers.Add(new UserBasicDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    FullName = user.FullName
                });
            }
        }
        
        
        return new ShiftDto
        {
            Id = shift.ID,
            Date = shift.Date,
            StartTime = shift.StartTime,
            EndTime = shift.EndTime,
            ShiftType = shift.ShiftType,
            AssignedUsers = assignedUsers
        };
    }
} 