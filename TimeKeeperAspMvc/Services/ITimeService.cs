using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TimeKeeperAspMvc.Models;

using TimeKeeperAspMvc.Data;

namespace TimeKeeperAspMvc.Services
{
    public interface ITimeService
    {
        DateTime GetTime();

        Task<AccessTime> LogTime(TimeKeeperContext context, DateTime time);
    }
}
