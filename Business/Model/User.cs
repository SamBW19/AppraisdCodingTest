namespace Business.Model
{
    public class User
    {
        public bool bPrivate;
        public string Email { get; set; }
        public int FirmID { get; set; }
        public int UserID { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }
    }
}
