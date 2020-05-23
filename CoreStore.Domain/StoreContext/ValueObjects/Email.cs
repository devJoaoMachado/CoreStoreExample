using FluentValidator;
using FluentValidator.Validation;

namespace CoreStore.Domain.StoreContext.ValueObjects
{
    public class Email : Notifiable
    {
        public Email(string address)
        {
            Address = address;

            AddNotifications(new ValidationContract()
                                 .Requires()
                                 .IsEmail(address, "Address", "E-mail inválido"));
                                
        }

        public string Address { get; set; }

        public override string ToString()
        {
            return Address;
        }
    }
}
