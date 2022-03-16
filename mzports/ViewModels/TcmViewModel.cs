using Communications;
using mzports.Services.TCM;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace mzports.ViewModels
{
    public class TcmViewModel : ViewModelBase
    {
        #region variables
        private readonly TemperatureControllerModule _tcm;
        private readonly ICommunication _com;

        public ObservableCollection<string> PortNames { get; private set; }
        public ObservableCollection<int> Buadrates { get; private set; }
        #endregion

        public TcmViewModel(TemperatureControllerModule tcm, ICommunication com)
        {
            _tcm = tcm;
            _com = com;
            _ = Task.Run(() => GetDevieNameAsync());
            PortNames = new ObservableCollection<string>();
            string[]? ports = SerialPort.GetPortNames();

            foreach (var item in ports)
            {
                PortNames.Add(item);
            }

            Buadrates = new ObservableCollection<int>
            {
                9600,
                115200
            };

            DispatcherTimer timer = new();
            timer.Interval = new System.TimeSpan(0, 0, 5);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        #region Properties
        private string deviceIsConnected;

        public string DeviceIsConnected
        {
            get => deviceIsConnected;
            set
            {
                deviceIsConnected = value;
                OnPropertyChanged();
            }
        }

        private bool portIsConnected;

        public bool PortIsConnected
        {
            get { return portIsConnected; }
            set
            {
                portIsConnected = value;
                OnPropertyChanged();
            }
        }

        private string selectedPort;

        public string SelectedPort
        {
            get => selectedPort;
            set
            {
                selectedPort = value;
                _com.ChangeSetting(GetSerialSetting());
                OnPropertyChanged();
            }
        }

        private int selectedBuadrate;

        public int SelectedBuadrate
        {
            get { return selectedBuadrate; }
            set
            {
                selectedBuadrate = value;
                _com.ChangeSetting(GetSerialSetting());
                OnPropertyChanged();
            }
        }


        private string? deviceName;

        public string? DeviceName
        {
            get => deviceName;
            set
            {
                deviceName = value;
                OnPropertyChanged();
            }
        }

        private double rangeFrom;

        public double RangeFrom
        {
            get => rangeFrom;
            set
            {
                rangeFrom = value;
                OnPropertyChanged();
            }
        }

        private double rangeTo;

        public double RangeTo
        {
            get => rangeTo;
            set
            {
                rangeTo = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Methods
        private async void Timer_Tick(object? sender, System.EventArgs e)
        {
            bool res = await _tcm.SelfCheck();

            DeviceIsConnected = res ? "Connected" : "Disconnected";
        }
        private async void GetDevieNameAsync()
        {
            await _tcm.SelfCheck();
        }
        private SerialSetting GetSerialSetting()
        {
            SerialSetting st = new SerialSetting()
            {
                PortName = selectedPort,
                Baudrate = selectedBuadrate
            };
            return st;

        }
        #endregion

        #region commands
        public ICommand RangeApplyCommand => new Commands.RelayCommand(() => RangeApply());
        private void RangeApply()
        {
            _tcm.SetTransmitterMin(rangeFrom);
            _tcm.SetTransmitterMax(rangeTo);
        }

        public ICommand PortConnectCommand => new Commands.RelayCommand(() => PortConect());
        private void PortConect()
        {
            if (selectedPort is null)
            {
                _ = MessageBox.Show("Select COM port, please!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (portIsConnected)
            {
                if (!(_com.Connect()))
                {
                    PortIsConnected = false;
                }
            }
            else
            {
                //Disconnect
            }
        }
        #endregion

    }
}
