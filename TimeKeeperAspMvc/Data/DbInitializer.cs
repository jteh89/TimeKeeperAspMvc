using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TimeKeeperAspMvc.Models;

namespace TimeKeeperAspMvc.Data
{
    public class DbInitializer
    {
        public static void Initialize(TimeKeeperContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
