using CoreStore.Domain.StoreContext.ValueObjects;
using FluentValidator;
using System.Collections.Generic;
using System.Linq;

namespace CoreStore.Domain.StoreContext.Entities
{
    public class Customer : Notifiable
    {
        private IList<Address> _addresses;
        private Cpf _cpf;
        private Name _name;
        private Email _email;
        private string _phone;

        public Customer(Name name,
                        Cpf document,
                        Email email,
                        string phone)
        {
            _name = name;
            _cpf = document;
            _email = email;
            _phone = phone;
            _addresses = new List<Address>();
        }

        public string Cpf => _cpf.Number;
        public string FirstName => _name.FirstName;
        public string LastName => _name.LastName;
        public string Email => _email.Address;
        public string Phone => _phone;

        public IReadOnlyCollection<Address> Addresses => _addresses.ToArray();

        public void AddAddress(Address address)
        {
            _addresses.Add(address);
        }

        public override string ToString()
        {
            return _name.ToString();
        }

    }
}
