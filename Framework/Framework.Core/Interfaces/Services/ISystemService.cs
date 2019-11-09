using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Interfaces.Services
{
    public interface ISystemService : IService
    {
        DateTime GetCurrentDateTime();
        DateTime GetCurrentDateTimeUTC();
    }
}
