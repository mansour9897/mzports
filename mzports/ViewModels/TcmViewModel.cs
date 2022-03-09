using Communications;
using mzports.Services.TCM;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace mzports.ViewModels
{
    public class TcmViewModel : ViewModelBase
    {
        private readonly TemperatureControllerModule _tcm;
        private readonly ICommunication _com;

        public ObservableCollection<string> PortNames { get; private set; }
        public TcmViewModel(TemperatureControllerModule tcm, ICommunication com)
        {
            _tcm = tcm;
            _com = com;
            _ = Task.Run(() => GetDevieNameAsync());
            PortNames = new ObservableCollection<string>();
            var ports = SerialPort.GetPortNames();
            foreach (var item in ports)
            {
                PortNames.Add(item);
            }
        }

        private string selectedPort;

        public string SelectedPort
        {
            get => selectedPort;
            set
            {
                selectedPort = value;
                SerialSetting st = new SerialSetting()
                {
                    PortName = selectedPort,
                    Baudrate = 9600
                };
                _com.ChangeSetting(st);
                OnPropertyChanged();
            }
        }


        private string? deviceNamr;

        public string? DeviceName
        {
            get => deviceNamr;
            set
            {
                deviceNamr = value;
                OnPropertyChanged();
            }
        }

        private async void GetDevieNameAsync()
        {
            _tcm.SelfCheck();
            await Task.Delay(1);
        }

        #region commands
        public ICommand RangeApplyCommand => new Commands.RelayCommand(() => RangeApply());
        private void RangeApply()
        {

        }

        public ICommand PortConnectCommand => new Commands.RelayCommand(() => PortConect());
        private void PortConect()
        {
            if (selectedPort is null)
            {
                _ = MessageBox.Show("Select COM port, please!","Error", MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }

            _tcm.SelfCheck();
        }
        #endregion

    }
}
