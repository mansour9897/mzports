using Communications;

namespace mzports.Services.TCM.DeviceCommands
{
    class TcmSelfCheckCommand : DeviceCommand, ITcmSelfCheckCommand
    {
        private const string code = "#0000\r";
        private const string confirm = "!0000";
        public TcmSelfCheckCommand(ICommunication com) : base(code, confirm, com: com)
        {
        }
    }
}
