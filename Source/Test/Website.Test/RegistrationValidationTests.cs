using System.Dynamic;
using Codalicious.TDDUtilities;
using NUnit.Framework;
using Rhino.Mocks;
using ServiceStack.FluentValidation.Results;
using Website.App;

namespace Website.Test
{
    [TestFixture]
    public class RegistrationValidationTests
    {
        private IRegistrationRepository _repository;
        private RegistrationValidation _sut;
        private dynamic _testContext;

        [SetUp]
        public void SetUp()
        {
            _testContext = new ExpandoObject();
            _repository = MockRepository.GenerateMock<IRegistrationRepository>();
            _sut = new RegistrationValidation(_repository);
        }

        [Test]
        public void it_should_not_allow_an_existing_email_to_register()
        {
            var email = RandomDataHelper.Get<string>();
            given_there_is_already_a_registration_for_email(email);
            when_registration_is_validated_for_email(email);
            then_the_validation_result_is_valid_should_be(false);
            and_the_error_message_should_be("The email is already registered.");
            and_the_error_code_should_be("Registered");
        }

        [Test]
        public void it_should_allow_an_email_that_has_not_yet_registered()
        {
            var email = RandomDataHelper.Get<string>();
            given_there_is_not_already_a_registration_for_email(email);
            when_registration_is_validated_for_email(email);
            Assert.That(_testContext.Result.IsValid, Is.True);
        }

        private void and_the_error_code_should_be(string code)
        {
            var result = _testContext.Result as ValidationResult;
            Assert.That(result.Errors[0].ErrorCode, Is.EqualTo(code));
        }

        private void and_the_error_message_should_be(string message)
        {
            var result = _testContext.Result as ValidationResult;
            Assert.That(result.Errors[0].ErrorMessage, Is.EqualTo(message));
        }

        private void then_the_validation_result_is_valid_should_be(bool isValid)
        {
            Assert.That(_testContext.Result.IsValid, Is.EqualTo(isValid));
        }

        private void when_registration_is_validated_for_email(string email)
        {
            var registration = new Registration { Email = "jimmy@funkyboss.com" };
            _testContext.Result = _sut.Validate(registration);
        }

        private void given_there_is_already_a_registration_for_email(string email)
        {
            _repository.Stub(r => r.FindByEmail("jimmy@funkyboss.com")).Return("guid");
        }

        private void given_there_is_not_already_a_registration_for_email(string email)
        {
            _repository.Stub(r => r.FindByEmail("jimmy@funkyboss.com")).Return(null);
        }
    }
}