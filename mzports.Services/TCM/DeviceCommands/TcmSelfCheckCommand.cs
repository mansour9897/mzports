using Communications;

namespace mzports.Services.TCM.DeviceCommands
{
    class TcmSelfCheckCommand : DeviceCommand
    {
        private const string code = "?01\r";
        private const string confirm = "!01";
        public TcmSelfCheckCommand(ICommunication com) : base(code, confirm, com: com)
        {
        }
    }
}
