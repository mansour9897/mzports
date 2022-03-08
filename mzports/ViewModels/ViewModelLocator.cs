using Microsoft.Extensions.DependencyInjection;

namespace mzports.ViewModels
{
    public class ViewModelLocator
    {
        public static TcmViewModel TcmViewModel => App.ServiceProvider.GetRequiredService<TcmViewModel>();
    }
}
