namespace BillBuddy.API.DTOs
{
    public class CreateSplitTransactionResponse
    {
        public Guid Id { get; set; }
        public Guid PublicIdentifier { get; set; }
        public string Title { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime TransactionDateTIme { get; set; }
        public DateTime SplitDateTIme { get; set; }
        public DateTime DueDateTime { get; set; }
        public CreateUserResponse PaidBy { get; set; }
        public CreateUserResponse CreatedBy { get; set; }
        public List<ParticipantDetails> Participants { get; set; }
    }
}
