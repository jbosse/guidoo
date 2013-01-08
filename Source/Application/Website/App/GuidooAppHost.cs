using ServiceStack.ServiceInterface.Validation;
using ServiceStack.WebHost.Endpoints;

namespace Website.App
{
    public class GuidooAppHost : AppHostBase
    {
        public GuidooAppHost() : base("Guidoo", typeof(Global).Assembly)
        {
        }

        public override void Configure(Funq.Container container)
        {
            container.RegisterAutoWiredAs<RegistrationRepository, IRegistrationRepository>();
            container.RegisterAutoWiredAs<GuidFactory, IGuidFactory>();

            Plugins.Add(new ValidationFeature());
            container.RegisterValidators(typeof (Global).Assembly);
        }
    }
}