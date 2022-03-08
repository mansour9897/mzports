using Communications;

namespace mzports.Services.TCM.DeviceCommands
{
    class RequestDeviceNameCommand : DeviceCommand
    {
        private const string code = "#0001\r";
        private const string confirm = "!0001";
        public RequestDeviceNameCommand(ICommunication com) : base(code, confirm, com)
        {

        }
    }
}
