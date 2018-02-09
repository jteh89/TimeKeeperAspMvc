using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TimeKeeperAspMvc.Data;
using TimeKeeperAspMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace TimeKeeperAspMvc.Services
{
    public class TimeService : ITimeService
    {
        public DateTime GetTime()
        {
            return DateTime.Now;
        }

        public async Task<AccessTime> LogTime(TimeKeeperContext context, DateTime time)
        {
            AccessTime newAccessTime = new AccessTime();
            newAccessTime.Time = time;
            context.Add(newAccessTime);
            await context.SaveChangesAsync();
            return newAccessTime;
        }

    }
}
