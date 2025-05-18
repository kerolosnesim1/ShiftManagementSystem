using ShiftManagementSystem.Application.DTOs.Users;

namespace ShiftManagementSystem.Application.DTOs.Shifts;

public class ShiftDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string ShiftType { get; set; } = string.Empty;
    public List<UserBasicDto> AssignedUsers { get; set; } = new List<UserBasicDto>();
} 