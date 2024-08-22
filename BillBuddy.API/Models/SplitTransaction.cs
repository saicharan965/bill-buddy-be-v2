namespace BillBuddy.API.Models;
public class SplitTransaction
{
    public Guid Id { get; set; }
    public Guid PublicIdentifier { get; set; }
    public string Title { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime TransactionDateTIme { get; set; }
    public DateTime SplitDateTIme { get; set; }
    public DateTime DueDateTime { get; set; }
    public Guid PaidBy { get; set; }
    public Guid CreatedBy { get; set; }
    public List<Guid> ParticipantPublicIdentifiers { get; set; }

}
