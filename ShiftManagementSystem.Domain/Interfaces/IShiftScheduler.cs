using ShiftManagmentSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IShiftScheduler
{
  Task<bool> AssignUserToShiftAsync(Guid userID , Guid shiftID);

  Task<bool> RemoveUSerFromShiftAsync(Guid userID , Guid shiftId);

  Task<bool> ISUserAvailableForShiftAsync(Guid userID , DataTime data, TimeSpan startTime, TimeSpan endTime); 
}
