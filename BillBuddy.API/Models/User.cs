namespace BillBuddy.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string ProfilePictureUrl { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
