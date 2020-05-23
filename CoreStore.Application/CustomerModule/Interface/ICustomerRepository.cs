using CoreStore.Application.CustomerModule.Dto;
using CoreStore.Domain.StoreContext.Entities;
using CoreStore.Infra.Data.Common;
using System.Threading.Tasks;

namespace CoreStore.Application.CustomerModule.Interface
{
    public interface ICustomerRepository
    {
        bool ExistDocument(string document);
        bool ExistEmail(string email);
        Task Save(Customer customer);
        Task<PagedView<CustomerDto>> GetPagedCustomers(string name, string cpf, string email, int page, int pageSize);

    }
}
