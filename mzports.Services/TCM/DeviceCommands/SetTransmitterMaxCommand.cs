using Communications;
using mzports.Services.Implementation;

namespace mzports.Services.TCM.DeviceCommands
{
    class SetTransmitterMaxCommand : SetValueCommand
    {
        private const string opcode = "?12";
        private const string confirm = "!12";
        public SetTransmitterMaxCommand(double value, ICommunication com) : base(opcode, confirm, value, com)
        {
        }

    }
}
