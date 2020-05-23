using CoreStore.Application.CustomerModule.Dto;
using CoreStore.Application.CustomerModule.Interface;
using CoreStore.Application.CustomerModule.Query;
using CoreStore.Shared.Command;
using CoreStore.Shared.Shared.Command;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CoreStore.Application.CustomerModule.QueryHandler
{
    public class CustomersQueryHandler : IRequestHandler<CustomersQuery, Result>
    {
        ICustomerRepository _customerRepository;

        public CustomersQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Result> Handle(CustomersQuery request, CancellationToken cancellationToken)
        {
            Result result = new Result();

            var pagedCustomers = await
                _customerRepository.GetPagedCustomers(request.Name, request.Cpf, request.Email, request.Page, request.PageSize);

            if (pagedCustomers.Data.Count > 0)
            {
                result = new ResultQuery<List<CustomerDto>>
                                    (pagedCustomers.Data,
                                     pagedCustomers.Page,
                                     pagedCustomers.PageSize,
                                     pagedCustomers.TotalItems,
                                     pagedCustomers.TotalPages
                                    );
            }
            else
            {
                result = new ResultQuery<List<CustomerDto>>();
                result.AddNotification(new FluentValidator.Notification("Query", "Nenhum resultado foi encontrado para os parâmetros de busca informados."));
            }

            return result;
        }
    }
}
