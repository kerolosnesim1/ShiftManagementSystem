using ShiftManagementSystem.Application.DTOs.Users;
using ShiftManagementSystem.Domain.Interfaces;

namespace ShiftManagementSystem.Application.Services.Users;

public class UserService{

    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository){
    
        _userRepository = userRepository;
    }

    public async Task<List<UserDto>> GetAllUsersAsync(){
    
        var users = await _userRepository.GetAllAsync();
        
        // Convert entities to DTOs using LINQ Select
        return users.Select(user => new UserDto{
        
            Id = user.Id,
            Name = user.Name,
            Email = user.Email ?? string.Empty,
            FullName = user.FullName,
            Role = user.Role,
            AssignedShiftIds = user.ShiftAssignments?.Select(sa => sa.ShiftId).ToList() ?? new List<Guid>()
        }).ToList();
    }

    public async Task<UserDto> GetUserByIdAsync(Guid id){
       
       var user = await _userRepository.GetByIdAsync(id);

       if(user == null){
        return null;
       }

       return new UserDto{
        Id = user.Id,
        Name = user.Name,
        Email = user.Email ?? string.Empty,
        FullName = user.FullName,
        Role = user.Role,
        AssignedShiftIds = user.ShiftAssignments?.Select(sa => sa.ShiftId).ToList() ?? new List<Guid>()
       };
    }
    
    public async Task<bool> UpdateUserAsync(Guid id, UserDto userDto){

      var existingUser = await _userRepository.GetByIdAsync(id);

      if (existingUser == null){
        return false;
      }      

      existingUser.Name = userDto.Name;
      existingUser.FullName = userDto.FullName;
      existingUser.Role = userDto.Role;

      await _userRepository.UpdateAsync(existingUser);

      return true;
      
    }
    
    public async Task<bool> DeleteUserAsync(Guid id){

      var user = await _userRepository.GetByIdAsync(id);

      if (user == null){
        return false;
      }

      await _userRepository.DeleteAsync(id);
      return true;
    }
}
