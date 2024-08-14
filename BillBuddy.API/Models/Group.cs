namespace BillBuddy.API.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Participants { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
