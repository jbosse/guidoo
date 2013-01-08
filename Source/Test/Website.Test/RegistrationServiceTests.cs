using System;
using System.Dynamic;
using System.Net;
using NUnit.Framework;
using Rhino.Mocks;
using ServiceStack.Common.Web;
using Website.App;
using Website.Services;

namespace Website.Test
{
    [TestFixture]
    public class RegistrationServiceTests
    {
        private IGuidFactory _guidFactory;
        private IRegistrationRepository _registrationRepository;
        private RegistrationService _sut;
        private Registration _request;
        private RegistrationResult _response;
        private dynamic _testContext;

        [SetUp]
        public void SetUp()
        {
            _testContext = new ExpandoObject();
            _testContext.Exception = null;
            _guidFactory = MockRepository.GenerateMock<IGuidFactory>();
            _registrationRepository = MockRepository.GenerateMock<IRegistrationRepository>();
            _sut = new RegistrationService(_guidFactory, _registrationRepository);
            _request = new Registration();
        }

        [Test]
        public void it_should_return_a_guid()
        {
            given_the_guid_factory_will_create_a_guid("e903fc3f-7022-4e50-868f-8bda9c448472");
            when_the_request_is_posted_to_the_service();
            then_the_response_should_have_the_guid("e903fc3f-7022-4e50-868f-8bda9c448472");
        }

        [Test]
        public void it_should_save_the_registration_information_in_the_repository()
        {
            given_the_guid_factory_will_create_a_guid("e903fc3f-7022-4e50-868f-8bda9c448472");
            and_the_registration_has_an_email("jimmy@codalicio.us");
            when_the_request_is_posted_to_the_service();
            then_the_repository_should_have_saved("jimmy@codalicio.us", "e903fc3f-7022-4e50-868f-8bda9c448472");
        }

        private void and_the_exception_message_should_be(string message)
        {
            var exception = (HttpError)_testContext.Exception;
            Assert.That(exception.Message, Is.EqualTo(message));
        }

        private void then_an_excpetion_should_have_been_thrown(HttpStatusCode statusCode)
        {
            var exception = (HttpError)_testContext.Exception;
            Assert.That(exception, Is.Not.Null);
            Assert.That(exception, Is.TypeOf<HttpError>());
            Assert.That(exception.StatusCode, Is.EqualTo(statusCode));
        }

        private void given_there_is_already_a_registration_for_email(string email)
        {
            _registrationRepository.Stub(r => r.FindByEmail(email)).Return("e903fc3f-7022-4e50-868f-8bda9c448472");
        }

        private void then_the_repository_should_have_saved(string email, string guid)
        {
            _registrationRepository.AssertWasCalled(r => r.Add(email, guid));
        }

        private void and_the_registration_has_an_email(string email)
        {
            _request.Email = email;
        }

        private void then_the_response_should_have_the_guid(string guid)
        {
            Assert.That(_response.Guid, Is.EqualTo(guid));
        }

        private void when_the_request_is_posted_to_the_service()
        {
            try
            {
                _response = (RegistrationResult)_sut.Post(_request);
            }
            catch (Exception e)
            {
                _testContext.Exception = e;
            }
        }

        private void given_the_guid_factory_will_create_a_guid(string guid)
        {
            _guidFactory.Stub(x => x.Create()).Return(Guid.Parse(guid));
        }
    }
}