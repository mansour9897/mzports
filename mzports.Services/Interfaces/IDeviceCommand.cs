using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mzports.Services
{
    interface IDeviceCommand
    {
        string CommandCode { get; }
        bool ExecuteConfirmed { get; set; }
        string ConfirmationCode { get; }

        void Execute();
    }
}
