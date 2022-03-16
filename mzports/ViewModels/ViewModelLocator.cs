using Microsoft.Extensions.DependencyInjection;

namespace mzports.ViewModels
{
    public class ViewModelLocator
    {
        public TcmViewModel TcmViewModel => App.ServiceProvider.GetRequiredService<TcmViewModel>();
    }
}
