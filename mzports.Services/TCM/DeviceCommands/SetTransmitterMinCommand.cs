using Communications;

namespace mzports.Services.TCM.DeviceCommands
{
    class SetTransmitterMinCommand : SetValueCommand
    {
        private const string opcode = "?11";
        private const string confirm = "!11";
        public SetTransmitterMinCommand(double setVal, ICommunication com) : base(opcode, confirm, setVal, com)
        {
        }

    }
}
