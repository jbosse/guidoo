using ServiceStack.ServiceHost;

namespace Website.App
{
    [Route("/registration")]
    [Route("/registration/{Email}")]
    public class Registration : IReturn<RegistrationResult>
    {
        public string Email { get; set; }
    }
}