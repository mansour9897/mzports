using Communications;

namespace mzports.Services.TCM.DeviceCommands
{
    class SetTransmitterMinCommand : DeviceCommand
    {
        private const string opcode = "?11";
        private const string confirm = "!11";
        public SetTransmitterMinCommand(double setVal, ICommunication com) : base(GetCode(setVal), GetConfirmCode(setVal), com)
        {
        }

        private static string GetCode(double val) => string.Format("{0}\t{1:0.00}\r", opcode, val);
        private static string GetConfirmCode(double val) => string.Format("{0}\t{1:0.00}\r", confirm, val);
    }
}
