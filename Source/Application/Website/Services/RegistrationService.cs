using System.Net;
using ServiceStack.Common.Web;
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
            if (_registrationRepository.FindByEmail(registration.Email) != null)
            {
                throw new HttpError(HttpStatusCode.Conflict, "The email is already registered.");
            }
            var guid = _guidFactory.Create();
            _registrationRepository.Add(registration.Email, guid.ToString());
            return new RegistrationResult { Guid = guid };
        }

        public object Get(Registration registration)
        {
            return Post(registration);
        }
    }
}