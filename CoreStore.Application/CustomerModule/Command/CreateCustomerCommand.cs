using CoreStore.Shared.Command;
using FluentValidator.Validation;
using MediatR;

namespace CoreStore.Application.CustomerModule.Command
{
    public class CreateCustomerCommand : ValidatableCommand, IRequest<Result>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public override void Validate()
        {
            AddNotifications(new ValidationContract()
                               .Requires()
                               .HasMinLen(FirstName, 3, "firstName", "O nome deve conter pelo menos 3 caracteres.")
                               .HasMaxLen(FirstName, 40, "firstName", "O nome deve conter no máximo 40 caracteres.")
                               .HasMinLen(LastName, 3, "LastName", "O sobrenome deve conter pelo menos 3 caracteres.")
                               .HasMaxLen(LastName, 60, "LastName", "O sobrenome deve conter no máximo 40 caracteres.")
                               .IsEmail(Email, "Email", "E-mail inválido"));
        }
    }
}
