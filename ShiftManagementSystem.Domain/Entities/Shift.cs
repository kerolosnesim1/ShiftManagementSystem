using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftManagementSystem.Domain.Entities;

public class Shift
{
    public Guid ID { get; set; } = Guid.NewGuid();
    public DateTime Date { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public string ShiftType { get; set; } = string.Empty;


    public ICollection<ShiftAssignment> ShiftAssignments { get; set; }
}
