namespace BillBuddy.API.Models
{
    public class Participant
    {
        public int Id { get; set; }
        public User UserDetails{ get; set; }
        public decimal ContributionAmount { get; set; }
    }
}
