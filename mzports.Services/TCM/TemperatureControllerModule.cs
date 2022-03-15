using Communications;
using mzports.Services.TCM.DeviceCommands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mzports.Services.TCM
{
    public class TemperatureControllerModule : IDevices
    {
        private readonly ICommunication _com;
        private readonly IDeviceCommand _tcmSelfCheckCommand;

        private IDeviceCommand _setTransmitterMinCmd;
        private IDeviceCommand _setTransmitterMaxCmd;

        private List<IDeviceCommand> commands;
        
        public TemperatureControllerModule(ICommunication com)
        {
            _com = com;
            _com.MessageReceived += Com_MessageReceived;
            _tcmSelfCheckCommand = new TcmSelfCheckCommand(_com);

            commands = new List<IDeviceCommand>()
            {
                _tcmSelfCheckCommand,
                _setTransmitterMinCmd,
                _setTransmitterMaxCmd
            };
        }

        private void Com_MessageReceived(object sender, string message)
        {
            //if (!(message.IndexOf(_tcmSelfCheckCommand.ConfirmationCode) < 0))
            //{
            //    _tcmSelfCheckCommand.ExecuteConfirmed = true;
            //}
            foreach (var cmd in commands)
            {
                if (cmd is null) continue;

                if(!(message.IndexOf(cmd.ConfirmationCode)<0))
                {
                    cmd.ExecuteConfirmed = true;
                }
            }
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
