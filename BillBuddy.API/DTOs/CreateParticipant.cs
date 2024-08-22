namespace BillBuddy.API.DTOs
{
    public class CreateParticipant
    {
        public Guid PublicIdentifier { get; set; }
        public decimal SplitAmount { get; set; }
    }
}
