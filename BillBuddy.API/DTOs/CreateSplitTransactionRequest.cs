namespace BillBuddy.API.DTOs
{
    public class CreateSplitTransactionRequest
    {
        public string Title { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime TransactionDateTIme { get; set; }
        public DateTime DueDateTime { get; set; }
        public Guid CreatedByPublicIdentifier { get; set; }
        public Guid PaidByPublicIdentifier { get; set; }
        public List<ParticipantRequest> Participants { get; set; }
    }
}
