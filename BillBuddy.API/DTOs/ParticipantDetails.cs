namespace BillBuddy.API.DTOs
{
    public class ParticipantDetails
    {
        public Guid PublicIdentifier { get; set; }
        public decimal SplitAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal BalanceAmount { get; set; }
        public Enums.SettlementStatus SettlementStatus { get; set; }
        public CreateUserResponse Participant { get; set; }
    }

}
