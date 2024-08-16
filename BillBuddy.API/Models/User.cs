﻿namespace BillBuddy.API.Models
{
    public class User
    {
        public string Guid { get; set; }
        public string InternalId { get; set; }
        public string PublicIndentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string ProfilePictureUrl { get; set; }
        public List<SplitTransaction> Transactions { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
    }
}
