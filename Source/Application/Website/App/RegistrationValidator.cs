using ServiceStack.FluentValidation;

namespace Website.App
{
    public class RegistrationValidator : AbstractValidator<Registration>
    {
        public IRegistrationRepository RegistrationRepository { get; set;}

        public RegistrationValidator()
        {
            RuleFor(r => r.Email).Must(x => RegistrationRepository.FindByEmail(x) == null);
        }
    }
}