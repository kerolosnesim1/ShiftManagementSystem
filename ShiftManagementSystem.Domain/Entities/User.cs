using ShiftManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftManagementSystem.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = String.Empty;
    public string FullName { get; set; } = String.Empty;
    public Role Role { get; set; }


    public ICollection<ShiftAssignment> ShiftAssignments { get; set; }    
}