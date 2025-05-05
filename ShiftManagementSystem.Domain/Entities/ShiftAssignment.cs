using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftManagementSystem.Domain.Entities;

    public class ShiftAssignment
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid ShiftId { get; set; }
    public Shift Shift { get; set; }

}