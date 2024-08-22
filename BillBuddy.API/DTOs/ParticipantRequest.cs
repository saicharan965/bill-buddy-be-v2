namespace BillBuddy.API.DTOs
{
    public class ParticipantRequest
    {
        public Guid PublicIdentifier { get; set; }
        public decimal SplitAmount { get; set; }
    }
}
