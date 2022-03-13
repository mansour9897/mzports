using System.Threading.Tasks;

namespace mzports.Services
{
    public interface IDevices
    {
        Task<bool> SelfCheck();
    }
}
