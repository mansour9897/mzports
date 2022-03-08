using mzports.Services.TCM;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Threading.Tasks;
using System.Windows.Input;

namespace mzports.ViewModels
{
    public class TcmViewModel : ViewModelBase
    {
        private readonly TemperatureControllerModule _tcm;

        public ObservableCollection<string> PortNames { get; private set; }
        public TcmViewModel(TemperatureControllerModule tcm)
        {
            _tcm = tcm;
            _ = Task.Run(() => GetDevieNameAsync());
            PortNames = new ObservableCollection<string>();
            var ports = SerialPort.GetPortNames();
            foreach (var item in ports)
            {
                PortNames.Add(item);
            }
        }

        public ICommand RangeApplyCommand => new Commands.RelayCommand(() => RangeApply());
        private void RangeApply()
        {

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

    }
}
