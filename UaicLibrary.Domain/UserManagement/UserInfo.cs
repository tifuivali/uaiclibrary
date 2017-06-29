namespace UaicLibrary.Domain.UserManagement
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public int ReadedBooks { get; set; }
        public int PublishedBooks { get; set; }
        public int OpennedBooks { get; set; }
        public string About { get; set; }
    }
}
