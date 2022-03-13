using Communications;
using mzports.Services.TCM.DeviceCommands;
using System.Threading.Tasks;

namespace mzports.Services.TCM
{
    public class TemperatureControllerModule : IDevices
    {
        private readonly ICommunication _com;
        private readonly IDeviceCommand _tcmSelfCheckCommand;
        //private readonly IDeviceCommand _tcmRequestDeviceNameCommand;
        public TemperatureControllerModule(ICommunication com)
        {
            _com = com;
            _com.MessageReceived += Com_MessageReceived;
            _tcmSelfCheckCommand = new TcmSelfCheckCommand(_com);
            //_tcmRequestDeviceNameCommand = new TcmSelfCheckCommand(_com);

        }

        private void Com_MessageReceived(object sender, string message)
        {
            if (!(message.IndexOf(_tcmSelfCheckCommand.ConfirmationCode) < 0))
            {
                _tcmSelfCheckCommand.ExecuteConfirmed = true;
            }
        }

        public async Task<bool> SelfCheck()
        {
            _tcmSelfCheckCommand.Execute();
            await Task.Delay(1);
            return _tcmSelfCheckCommand.ExecuteConfirmed;
        }


    }
}
