using System;
using System.Collections.Generic;
using System.Dynamic;
using NUnit.Framework;
using ServiceStack.CacheAccess;
using Website.App;

namespace Website.Test
{
    [TestFixture]
    public class RegistrationRepositoryTests
    {
        private IRegistrationRepository _sut;
        private dynamic _testContext;
        private ISession _session;

        [SetUp]
        public void SetUp()
        {
            _session = new Session();
            _testContext = new ExpandoObject();
            _sut = new RegistrationRepository();
            ((RegistrationRepository)_sut).Session = _session;
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
            _sut.Add(email, guid);
        }

        private class Session : ISession
        {
            public Session()
            {
                _session = new Dictionary<string, object>();
            }
            private readonly Dictionary<string, object> _session;

            public void Set<T>(string key, T value)
            {
                _session.Add(key, value);
            }

            public T Get<T>(string key)
            {
                if (_session.ContainsKey(key))
                {
                    return (T) _session[key];
                }
                return default(T);
            }

            public object this[string key]
            {
                set
                {
                    if (value == null) throw new ArgumentNullException("value");
                    _session.Add(key, this);
                }
                get { return _session[key]; }
            }
        }
    }
}