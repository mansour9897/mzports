using System.Windows.Input;

namespace mzports.ViewModels
{
    public class TcmViewModel : ViewModelBase
    {
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

    }
}
