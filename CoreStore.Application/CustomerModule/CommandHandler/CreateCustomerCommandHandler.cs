using CoreStore.Application.CustomerModule.Command;
using CoreStore.Application.CustomerModule.Interface;
using CoreStore.Domain.StoreContext.Entities;
using CoreStore.Domain.StoreContext.ValueObjects;
using CoreStore.Shared.Command;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace CoreStore.Application.CustomerModule.CommandHandler
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Result>
    {
        ICustomerRepository _customerRepository;
        IMediator _mediator;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IMediator mediator)
        {
            _customerRepository = customerRepository;
            _mediator = mediator;
        }

        public async Task<Result> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            Result result = new Result();

            var name = new Name(request.FirstName, request.LastName);
            var cpf = new Cpf(request.Cpf);
            var email = new Email(request.Email);

            result.AddNotifications(name.Notifications);
            result.AddNotifications(cpf.Notifications);
            result.AddNotifications(email.Notifications);

            if (result.Notifications.Any())
                return result;

            if (_customerRepository.ExistDocument(cpf.Number))
            {
                result.AddNotification(new FluentValidator.Notification("Cpf", "Cpf já cadastrado!"));
                return result;
            }

            if (_customerRepository.ExistEmail(email.Address))
            {
                result.AddNotification(new FluentValidator.Notification("Email", "E-mail já cadastrado!"));
                return result;
            }

            var customer = new Customer(name, cpf, email, request.Phone);
            await _customerRepository.Save(customer);
            result = new ResultCommand<string>("Cliente cadastrado com sucesso!");

            return result;
        }
    }
}
