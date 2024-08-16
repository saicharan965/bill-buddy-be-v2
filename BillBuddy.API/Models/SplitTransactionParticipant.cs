namespace BillBuddy.API.Models
{
    public class SplitTransactionParticipant
    {
        public string Guid { get; set; }
        public string InternalId { get; set; }
        public string PublicIndentifier { get; set; }
        public User Participant { get; set; }
        public decimal SplitAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal BalanceAmount { get; set; }
        public DateTime LastPaidDate { get; set; }
        public string SettleMentStatus { get; set; }
    }
}
