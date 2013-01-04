namespace Website.App
{
    public interface IRegistrationRepository
    {
        void Add(string email, string guid);
        string FindByEmail(string email);
    }
}