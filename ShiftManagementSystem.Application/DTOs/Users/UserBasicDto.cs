namespace ShiftManagementSystem.Application.DTOs.Users;

public class UserBasicDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
} 