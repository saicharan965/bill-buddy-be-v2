namespace BillBuddy.API.DTOs
{
    public class SplitTransactionParticipantDetails
    {
        public string PublicIndentifier { get; set; }
        public UserDetails Participant { get; set; }
        public decimal SplitAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal BalanceAmount { get; set; }
        public DateTime LastPaidDate { get; set; }
        public string SettleMentStatus { get; set; }
    }
}
