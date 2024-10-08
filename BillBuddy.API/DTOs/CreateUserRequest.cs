﻿namespace BillBuddy.API.DTOs
{
    public class CreateUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string Auth0Identifier { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
