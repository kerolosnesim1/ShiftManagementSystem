using ShiftManagementSystem.Domain.Enums;

namespace ShiftManagementSystem.Application.DTOs.Users;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public Role Role { get; set; }
    public List<Guid> AssignedShiftIds { get; set; } = new List<Guid>();
} 