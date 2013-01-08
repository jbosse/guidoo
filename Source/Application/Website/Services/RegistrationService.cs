using ServiceStack.ServiceInterface;
using Website.App;

namespace Website.Services
{
    public sealed class RegistrationService : Service
    {
        private readonly IGuidFactory _guidFactory;
        private readonly IRegistrationRepository _registrationRepository;

        public RegistrationService(IGuidFactory guidFactory, IRegistrationRepository registrationRepository)
        {
            _guidFactory = guidFactory;
            _registrationRepository = registrationRepository;
        }

        public object Post(Registration registration)
        {
            var guid = _guidFactory.Create().ToString("D");
            _registrationRepository.Add(registration.Email, guid);
            return new RegistrationResult { Guid = guid };
        }

        public object Get(Registration registration)
        {
            return Post(registration);
        }
    }
}