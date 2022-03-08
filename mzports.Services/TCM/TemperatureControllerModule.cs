using Communications;
using mzports.Services.TCM.DeviceCommands;

namespace mzports.Services.TCM
{
    public class TemperatureControllerModule : IDevices
    {
        private readonly ICommunication _com;
        private readonly ITcmSelfCheckCommand _tcmSelfCheckCommand;

        public TemperatureControllerModule(ICommunication com)
        {
            _com = com;
            _tcmSelfCheckCommand = new TcmSelfCheckCommand(_com);

        }

        public bool SelfCheck()
        {
            _tcmSelfCheckCommand.Execute();
            return _tcmSelfCheckCommand.ExecuteConfirmed;
        }
    }
}
