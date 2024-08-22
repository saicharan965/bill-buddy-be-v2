namespace BillBuddy.API.DTOs
{
    public class UserDetailsResponse
    {
        public Guid PublicIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string ProfilePictureUrl { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
