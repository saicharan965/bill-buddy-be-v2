using BillBuddy.API.Enums;

namespace BillBuddy.API.DTOs
{
    public class SplitTransactionParticipantDetails
    {
        public Guid PublicIdentifier { get; set; }
        public UserDetails Participant { get; set; }
        public decimal SplitAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal BalanceAmount { get; set; }
        public DateTime LastPaidDate { get; set; }
        public SettlementStatus SettleMentStatus { get; set; }
    }
}
