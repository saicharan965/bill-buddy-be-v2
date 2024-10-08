﻿using BillBuddy.API.Enums;
namespace BillBuddy.API.Models
{
    public class SplitTransactionParticipant
    {
        public int Id { get; set; }
        public Guid SplitTransactionParticipantIdentifier { get; set; }
        public Guid PublicIdentifier { get; set; }
        public decimal SplitAmount { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal BalanceAmount { get; set; }
        public DateTime LastPaidDate { get; set; }
        public SettlementStatus SettlementStatus { get; set; }
        public virtual User Participant { get; set; }
    }
}
