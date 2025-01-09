using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Work_Mentoring_Project
{
    internal interface IRegistryValues
    {
        string DeviceTypeID{ get; }
        string DeviceInstanceID { get; }
        string HardwareID { get; }
        public void Print();
    }
}
