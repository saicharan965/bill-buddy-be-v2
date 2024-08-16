using BillBuddy.API.Data;
using BillBuddy.API.DTOs;
using BillBuddy.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BillBuddy.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SplitTransactionController : ControllerBase
    {
        private readonly ApplicationDBContext _appDbContext;

        public SplitTransactionController(ApplicationDBContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<ActionResult<SplitTransactionDetails>> CreateSplitTransaction([FromBody] SplitTransactionDetails splitTransactionDto, CancellationToken cancellationToken)
        {
            if (splitTransactionDto == null)
            {
                return BadRequest("SplitTransaction cannot be null.");
            }

            if (splitTransactionDto.TotalAmount <= 0)
            {
                return BadRequest("TotalAmount must be greater than zero.");
            }

            if (splitTransactionDto.Participants == null || splitTransactionDto.Participants.Count == 0)
            {
                return BadRequest("At least one participant is required.");
            }

            var createdByUser = await _appDbContext.Users.FindAsync(splitTransactionDto.CreatedBy.PublicIndentifier, cancellationToken);

            var splitTransaction = new SplitTransaction
            {
                Id = Guid.NewGuid(),
                PublicIdentifier = Guid.NewGuid(),
                Title = splitTransactionDto.Title,
                TotalAmount = splitTransactionDto.TotalAmount,
                TransactionDateTIme = splitTransactionDto.TransactionDateTIme,
                SplitDateTIme = DateTime.UtcNow,
                DueDateTime = splitTransactionDto.DueDateTime,
                PaidBy = await _appDbContext.Users.FindAsync(splitTransactionDto.PaidBy.PublicIndentifier, cancellationToken),
                CreatedBy = createdByUser,
                Participants = splitTransactionDto.Participants.Select(p => new SplitTransactionParticipant
                {
                    SplitTransactionParticipantIdentifier = Guid.NewGuid(),
                    PublicIdentifier = p.PublicIdentifier,
                    SplitAmount = p.SplitAmount,
                    AmountPaid = p.AmountPaid,
                    BalanceAmount = p.BalanceAmount,
                    LastPaidDate = p.LastPaidDate,
                    SettlementStatus = p.SettleMentStatus,
                    Participant = _appDbContext.Users.Find(p.Participant.PublicIndentifier)
                }).ToList()
            };

            _appDbContext.SplitTransactions.Add(splitTransaction);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            var createdSplitTransactionDto = new SplitTransactionDetails
            {
                Id = splitTransaction.Id,
                PublicIdentifier = splitTransaction.PublicIdentifier,
                Title = splitTransaction.Title,
                TotalAmount = splitTransaction.TotalAmount,
                TransactionDateTIme = splitTransaction.TransactionDateTIme,
                SplitDateTIme = splitTransaction.SplitDateTIme,
                DueDateTime = splitTransaction.DueDateTime,
                PaidBy = new UserDetails
                {
                    PublicIndentifier = splitTransaction.PaidBy.PublicIndentifier,
                    FirstName = splitTransaction.PaidBy.FirstName,
                    LastName = splitTransaction.PaidBy.LastName,
                    EmailId = splitTransaction.PaidBy.EmailId,
                    ProfilePictureUrl = splitTransaction.PaidBy.ProfilePictureUrl
                },
                CreatedBy = new UserDetails
                {
                    PublicIndentifier = splitTransaction.CreatedBy.PublicIndentifier,
                    FirstName = splitTransaction.CreatedBy.FirstName,
                    LastName = splitTransaction.CreatedBy.LastName,
                    EmailId = splitTransaction.CreatedBy.EmailId,
                    ProfilePictureUrl = splitTransaction.CreatedBy.ProfilePictureUrl
                },
                Participants = splitTransaction.Participants.Select(p => new SplitTransactionParticipantDetails
                {
                    PublicIdentifier = p.PublicIdentifier,
                    SplitAmount = p.SplitAmount,
                    AmountPaid = p.AmountPaid,
                    BalanceAmount = p.BalanceAmount,
                    LastPaidDate = p.LastPaidDate,
                    SettleMentStatus = p.SettlementStatus,
                    Participant = new UserDetails
                    {
                        PublicIndentifier = p.Participant.PublicIndentifier,
                        FirstName = p.Participant.FirstName,
                        LastName = p.Participant.LastName,
                        EmailId = p.Participant.EmailId,
                        ProfilePictureUrl = p.Participant.ProfilePictureUrl
                    }
                }).ToList()
            };

            return CreatedAtAction(nameof(GetSplitTransactionById), new { id = createdSplitTransactionDto.PublicIdentifier }, createdSplitTransactionDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SplitTransactionDetails>> GetSplitTransactionById(Guid id, CancellationToken cancellationToken)
        {
            var splitTransaction = await _appDbContext.SplitTransactions
                .Include(st => st.Participants)
                .ThenInclude(p => p.Participant)
                .FirstOrDefaultAsync(st => st.PublicIdentifier == id, cancellationToken);

            if (splitTransaction == null)
            {
                return NotFound($"SplitTransaction with PublicIdentifier {id} was not found.");
            }

            var splitTransactionDto = new SplitTransactionDetails
            {
                Id = splitTransaction.Id,
                PublicIdentifier = splitTransaction.PublicIdentifier,
                Title = splitTransaction.Title,
                TotalAmount = splitTransaction.TotalAmount,
                TransactionDateTIme = splitTransaction.TransactionDateTIme,
                SplitDateTIme = splitTransaction.SplitDateTIme,
                DueDateTime = splitTransaction.DueDateTime,
                PaidBy = new UserDetails
                {
                    PublicIndentifier = splitTransaction.PaidBy.PublicIndentifier,
                    FirstName = splitTransaction.PaidBy.FirstName,
                    LastName = splitTransaction.PaidBy.LastName,
                    EmailId = splitTransaction.PaidBy.EmailId,
                    ProfilePictureUrl = splitTransaction.PaidBy.ProfilePictureUrl
                },
                Participants = splitTransaction.Participants.Select(p => new SplitTransactionParticipantDetails
                {
                    PublicIdentifier = p.PublicIdentifier,
                    SplitAmount = p.SplitAmount,
                    AmountPaid = p.AmountPaid,
                    BalanceAmount = p.BalanceAmount,
                    LastPaidDate = p.LastPaidDate,
                    SettleMentStatus = p.SettlementStatus,
                    Participant = new UserDetails
                    {
                        PublicIndentifier = p.Participant.PublicIndentifier,
                        FirstName = p.Participant.FirstName,
                        LastName = p.Participant.LastName,
                        EmailId = p.Participant.EmailId,
                        ProfilePictureUrl = p.Participant.ProfilePictureUrl
                    }
                }).ToList()
            };

            return Ok(splitTransactionDto);
        }
    }
}
