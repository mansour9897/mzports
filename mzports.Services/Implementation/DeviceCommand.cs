using Communications;
using System;
using System.Diagnostics;
using System.Threading;

namespace mzports.Services
{
    class DeviceCommand : IDeviceCommand
    {
        private readonly string _commandCode;
        private readonly string _confirmationCode;
        private readonly ICommunication _com;

        public DeviceCommand(string code, string confirmCode, ICommunication com)
        {
            _com = com;
            _commandCode = code;
            _confirmationCode = code;
        }

        public DeviceCommand(string code, ICommunication com)
        {
            _com = com;
            _commandCode = code;
            _confirmationCode = "";
        }

        public string CommandCode => _commandCode;

        public bool ExecuteConfirmed { get; set; }

        public string ConfirmationCode => _confirmationCode;

        public void Execute()
        {
            ExecuteConfirmed = false;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while (!ExecuteConfirmed)
            {
                _com.Write(_commandCode);
                Thread.Sleep(10);

                if (stopwatch.Elapsed.TotalMilliseconds > 3000)
                    break;
            }
        }
    }
}
