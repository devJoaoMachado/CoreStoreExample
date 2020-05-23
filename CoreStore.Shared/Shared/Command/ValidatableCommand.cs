using FluentValidator;

namespace CoreStore.Shared.Command
{
    public abstract class ValidatableCommand : Notifiable
    {
        public abstract void Validate();
     
    }
}
