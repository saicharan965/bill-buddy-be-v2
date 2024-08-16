namespace BillBuddy.API.Models
{
    public class User
    {
        public required int UserId { get; set; }
        public required Guid PublicIndentifier { get; set; }
        public required string Auth0Identifier { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string EmailId { get; set; }
        public required string ProfilePictureUrl { get; set; }
        public required bool IsDeleted { get; set; }
        public required bool IsActive { get; set; }
    }
}
