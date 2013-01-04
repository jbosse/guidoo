using System.Dynamic;
using Codalicious.TDDUtilities;
using NUnit.Framework;
using Rhino.Mocks;
using Website.App;

namespace Website.Test
{
    [TestFixture]
    public class RegistrationValidatorTests : TestFixtureBase
    {
        private dynamic _testContext;
        private RegistrationValidator _sut;
        private IRegistrationRepository _registrationRepository;

        public RegistrationValidatorTests()
        {
            this.Describes("Registration Validator");
        }

        [SetUp]
        public void SetUp()
        {
            _testContext = new ExpandoObject();
            _registrationRepository = MockRepository.GenerateMock<IRegistrationRepository>();
            _sut = new RegistrationValidator {RegistrationRepository = _registrationRepository};
        }

        [Test]
        public void it_should_not_be_valid_if_the_email_already_registered()
        {
            given_there_is_already_a_registration_for("jimmy@codalicio.us");
            when_valiating_the_request(new Registration {Email = "jimmy@codalicio.us"});
            then_the_validation_should_be(false);
        }

        [Test]
        public void it_shoud_be_valid_if_the_email_is_not_already_registered()
        {
            given_there_is_already_a_registration_for("jimmy@funkyboss.com");
            when_valiating_the_request(new Registration { Email = "jimmy@codalicio.us" });
            then_the_validation_should_be(true);
        }

        private void then_the_validation_should_be(bool isValid)
        {
            Assert.That(_testContext.Result.IsValid, Is.EqualTo(isValid));
        }

        private void when_valiating_the_request(Registration registration)
        {
            _testContext.Result = _sut.Validate(registration);
        }

        private void given_there_is_already_a_registration_for(string email)
        {
            _registrationRepository.Stub(r => r.FindByEmail(Arg<string>.Is.NotEqual(email))).Return(null);
            _registrationRepository.Stub(r => r.FindByEmail(email)).Return("e903fc3f-7022-4e50-868f-8bda9c448472");
        }
    }
}