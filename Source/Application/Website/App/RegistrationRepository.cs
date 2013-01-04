using System.Collections.Generic;
using ServiceStack.CacheAccess;

namespace Website.App
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly ISession _session;
        private readonly List<RegistrationDocument> _regisrations;

        public RegistrationRepository(ISession session)
        {
            _session = session;
            _regisrations = _session.Get<List<RegistrationDocument>>("registration") ?? new List<RegistrationDocument>();
        }

        public void Add(string email, string guid)
        {
            _regisrations.Add(new RegistrationDocument {Email = email, Guid = guid});
            _session.Set("registration", _regisrations);
        }

        public string FindByEmail(string email)
        {
            return _regisrations.Find(r => r.Email == email).Guid;
        }
    }
}