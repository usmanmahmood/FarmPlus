using Framework.Core.Interfaces.Services;
using System;

namespace Framework.Core.Services
{
    class SystemService : ISystemService
    {
        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }

        public DateTime GetCurrentDateTimeUTC()
        {
            return DateTime.UtcNow;
        }
    }
}
