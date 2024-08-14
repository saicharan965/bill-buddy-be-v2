namespace BillBuddy.API.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime DateTIme { get; set; }
        public User PaidBy { get; set; }
        public List<Participant> participants { get; set; }

    }
}
