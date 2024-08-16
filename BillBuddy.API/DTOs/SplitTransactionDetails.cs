namespace BillBuddy.API.Models
{
    public class SplitTransactionDetails
    {
        public string Guid { get; set; }
        public string InternalId { get; set; }
        public string PublicIndentifier { get; set; }
        public string Title { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime TransactionDateTIme { get; set; }
        public DateTime SplitDateTIme { get; set; }
        public DateTime DueDateTime { get; set; }
        public User PaidBy { get; set; }
        public List<SplitTransactionParticipant> Participants { get; set; }
    }
}
