using System;
using ServiceStack.ServiceInterface.ServiceModel;

namespace Website.App
{
    public class RegistrationResult
    {
        public Guid Guid { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}