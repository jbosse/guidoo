using System.Dynamic;
using System.Linq;
using NUnit.Framework;
using Raven.Client.Document;
using Raven.Client.Linq;
using Website.App;

namespace Website.Test
{
    [TestFixture]
    public class RegistrationRepositoryTests
    {
        private IRegistrationRepository _sut;
        private dynamic _testContext;

        [SetUp]
        public void SetUp()
        {
            _testContext = new ExpandoObject();
            _sut = new RegistrationRepository();
        }

        [TearDown]
        public void TearDown()
        {
            string email = _testContext.Email;
            using (var session = new DocumentStore { Url = "http://localhost:8080" }.Initialize().OpenSession())
            {
                var registrationDocument = (from d in session.Query<RegistrationDocument>()
                                            where d.Email == email
                                            select d).FirstOrDefault();
                session.Delete(registrationDocument);
                session.SaveChanges();
            }
        }

        [Test]
        public void it_should_get_data_by_email()
        {
            given_a_registration_has_been_added_with("jimmy@codalicio.us", "e903fc3f-7022-4e50-868f-8bda9c448472");
            when_the_repository_is_searched_for_the_email("jimmy@codalicio.us");
            then_it_should_return_the_guid("e903fc3f-7022-4e50-868f-8bda9c448472");
        }

        private void then_it_should_return_the_guid(string guid)
        {
            Assert.That(_testContext.Result.ToString(), Is.EqualTo(guid));
        }

        private void when_the_repository_is_searched_for_the_email(string email)
        {
            _testContext.Result = _sut.FindByEmail(email);
        }

        private void given_a_registration_has_been_added_with(string email, string guid)
        {
            _testContext.Email = email;
            _sut.Add(email, guid);
        }
    }
}