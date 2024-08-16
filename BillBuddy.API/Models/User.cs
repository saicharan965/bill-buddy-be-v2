namespace BillBuddy.API.Models
{
    public class User
    {
        public int UserId { get; set; }
        public Guid PublicIndentifier { get; set; }
        public string Auth0Identifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string ProfilePictureUrl { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
