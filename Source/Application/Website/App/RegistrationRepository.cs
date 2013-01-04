using System;
using System.Collections.Generic;
using ServiceStack.CacheAccess;

namespace Website.App
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private List<RegistrationDocument> Regisrations
        {
            get
            {
                var reg = Session.Get<List<RegistrationDocument>>("registration");
                return reg ?? new List<RegistrationDocument>();
            }
        }

        public ISession Session { get; set; }

        public void Add(string email, string guid)
        {
            var reg = Regisrations;
            reg.Add(new RegistrationDocument {Email = email, Guid = guid});
            Session.Set("registration", reg);
        }

        public string FindByEmail(string email)
        {
            var registrationDocument = Regisrations.Find(r => r.Email == email);
            return registrationDocument == null ? null : registrationDocument.Guid;
        }
    }
}