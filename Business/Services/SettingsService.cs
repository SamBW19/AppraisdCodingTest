using Business.Model;

namespace Business.Services
{
    public interface ISettingsService
    {
        ISettings Settings { get; }
    }
    public class SettingsService : ISettingsService
    {
        //public Settings Settings => new Settings {};
        public ISettings Settings => new Settings ();
    }
}
