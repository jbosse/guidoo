using System;
using NUnit.Framework;
using Rhino.Mocks;
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

        [SetUp]
        public void SetUp()
        {
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
            Assert.That(_response.Guid.ToString(), Is.EqualTo(guid));
        }

        private void when_the_request_is_posted_to_the_service()
        {
            _response = (RegistrationResult)_sut.Post(_request);
        }

        private void given_the_guid_factory_will_create_a_guid(string guid)
        {
            _guidFactory.Stub(x => x.Create()).Return(Guid.Parse(guid));
        }
    }
}