using FluentValidator;
using FluentValidator.Validation;

namespace CoreStore.Domain.StoreContext.ValueObjects
{
    public class Name : Notifiable
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new ValidationContract()
                                .Requires()
                                .HasMinLen(firstName, 3, "firstName", "O nome deve conter pelo menos 3 caracteres.")
                                .HasMaxLen(firstName, 40, "firstName", "O nome deve conter no máximo 40 caracteres.")
                                .HasMinLen(lastName, 3, "firstName", "O sobrenome deve conter pelo menos 3 caracteres.")
                                .HasMaxLen(lastName, 60, "firstName", "O sobrenome deve conter no máximo 40 caracteres."));
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
