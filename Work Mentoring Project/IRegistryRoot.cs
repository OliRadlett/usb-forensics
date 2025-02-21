using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work_Mentoring_Project
{
    public interface IRegistryRoot
    {
        IRegistryKey GetRegistry(string key);
    }
}
