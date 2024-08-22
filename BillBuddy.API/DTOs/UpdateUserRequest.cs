namespace BillBuddy.API.DTOs
{
    public class UpdateUserRequest
    {
        public Guid PublicIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string ProfilePictureUrl { get; set; }
    }
}
