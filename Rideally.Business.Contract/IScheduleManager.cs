using Rideally.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rideally.Business.Contract
{
    public interface IScheduleManager
    {
        List<Schedule> GetAllSchedule();
        bool AddSchedule(Schedule schedule);
        Schedule GetScheduleByID(int id);

        bool Update(Schedule sch);

        Schedule GetScheduleByEmployeeID(int id);
    }
}
