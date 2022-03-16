using Communications;
using mzports.Services.TCM.DeviceCommands;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace mzports.Services.TCM
{
    public class TemperatureControllerModule : IDevices
    {
        private readonly ICommunication _com;
        private readonly IDeviceCommand _tcmSelfCheckCommand;

        private IDeviceCommand _setTransmitterMinCmd;
        private IDeviceCommand _setTransmitterMaxCmd;

        public TemperatureControllerModule(ICommunication com)
        {
            _com = com;
            _tcmSelfCheckCommand = new TcmSelfCheckCommand(_com);
        }

        public async Task<bool> SelfCheck()
        {
            _tcmSelfCheckCommand.Execute();
            await Task.Delay(1);
            return _tcmSelfCheckCommand.ExecuteConfirmed;
        }

        public void SetTransmitterMin(double setValue)
        {
            _setTransmitterMinCmd = new SetTransmitterMinCommand(setValue, _com);
            _setTransmitterMinCmd.Execute();
        }

        public void SetTransmitterMax(double setValue)
        {
            _setTransmitterMaxCmd = new SetTransmitterMaxCommand(setValue, _com);
            _setTransmitterMaxCmd.Execute();
        }

    }
}
