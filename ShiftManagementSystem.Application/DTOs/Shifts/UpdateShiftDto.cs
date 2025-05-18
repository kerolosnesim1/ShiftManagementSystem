namespace ShiftManagementSystem.Application.DTOs.Shifts;

public class UpdateShiftDto
{
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string ShiftType { get; set; } = string.Empty;
} 