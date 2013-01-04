using ServiceStack.ServiceHost;

namespace Website.App
{
    [Route("/registration", "POST")]
    public class Registration : IReturn<RegistrationResult>
    {
        public string Email { get; set; }
    }
}