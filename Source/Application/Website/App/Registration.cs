using ServiceStack.ServiceHost;

namespace Website.App
{
    [Route("/registration", "POST")]
    [Route("/registration/{Email}", "GET")]
    public class Registration : IReturn<RegistrationResult>
    {
        public string Email { get; set; }
    }
}