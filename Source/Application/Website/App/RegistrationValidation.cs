using ServiceStack.FluentValidation;

namespace Website.App
{
    public class RegistrationValidation : AbstractValidator<Registration>
    {
        private readonly IRegistrationRepository _registrationRepository;

        public RegistrationValidation(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
            RuleFor(r => r.Email).
                Must(r => _registrationRepository.FindByEmail(r) == null).
                WithMessage("The email is already registered.").
                WithErrorCode("Registered");
        }
    }
}