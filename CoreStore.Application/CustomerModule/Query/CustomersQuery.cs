using CoreStore.Shared.Command;
using FluentValidator;
using MediatR;

namespace CoreStore.Application.CustomerModule.Query
{
    public class CustomersQuery : ValidatableCommand, IRequest<Result>
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public CustomersQuery()
        {
            Page = 1;
            PageSize = 10;
        }

        public override void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name) && string.IsNullOrWhiteSpace(Cpf) && string.IsNullOrWhiteSpace(Email))
            {
                AddNotification(new Notification("Query", "Um valor de busca deve ser informado!"));
            }
        }
    }
}
