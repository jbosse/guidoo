using System;
using ServiceStack.ServiceInterface.ServiceModel;

namespace Website.App
{
    public class RegistrationResult
    {
        public string Guid { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}