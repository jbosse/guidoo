using ServiceStack.ServiceInterface;
using Website.App;

namespace Website.Services
{
    public sealed class RegistrationService : Service
    {
        private readonly IGuidFactory _guidFactory;
        private readonly IRegistrationRepository _registrationRepository;

        public RegistrationService() : this(new GuidFactory(), null)
        {
        }

        public RegistrationService(IGuidFactory guidFactory, IRegistrationRepository registrationRepository)
        {
            _guidFactory = guidFactory;
            _registrationRepository = registrationRepository ?? new RegistrationRepository(Session);
        }

        public object Post(Registration registration)
        {
            var guid = _guidFactory.Create();
            _registrationRepository.Add(registration.Email, guid.ToString());
            return new RegistrationResult {Guid = guid};
        }
    }
}