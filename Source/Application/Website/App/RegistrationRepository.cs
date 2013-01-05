using System.Linq;
using Raven.Client.Document;
using Raven.Client.Linq;

namespace Website.App
{
    public class RegistrationRepository : IRegistrationRepository
    {
        public void Add(string email, string guid)
        {
            using (var session = new DocumentStore { ConnectionStringName = "RavenHQ" }.Initialize().OpenSession())
            {
                session.Store(new RegistrationDocument { Email = email, Guid = guid });
                session.SaveChanges();
            }
        }

        public string FindByEmail(string email)
        {
            using (var session = new DocumentStore { ConnectionStringName = "RavenHQ" }.Initialize().OpenSession())
            {
                var registrationDocument = (from d in session.Query<RegistrationDocument>()
                                            where d.Email == email
                                            select d).FirstOrDefault();
                return registrationDocument == null ? null : registrationDocument.Guid;
            }
        }
    }
}