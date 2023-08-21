using Business.Model;

namespace Business.Services
{
    public interface IRequestDataService
    {
        bool ModelStateIsValid { get; }
        User CurrentUser { get; } //confusing?

        AlternativeUser AlternativeCurrentUser { get; }
    }

    public class RequestDataService : IRequestDataService
    {
        public bool ModelStateIsValid => true;

        public User CurrentUser => new User() { UserID = 123 };

        public AlternativeUser AlternativeCurrentUser => new AlternativeUser() { UserID = 321 };
    }
}