using System.Threading.Tasks;

namespace CoreStore.Domain.StoreContext.Services
{
    public interface IServiceBusTopic
    {
        Task SendMessagesAsync(string json);
    }
}
