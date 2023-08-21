namespace Business.Model
{

    public interface ISettings
    {
        bool OnlySuperAdminsCanAddAppraisals { get; set; }
    }
    public class Settings : ISettings
    {

        public bool OnlySuperAdminsCanAddAppraisals { get; set; }
    }
}
