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
        public async Task<ActionResult<CreateSplitTransactionResponse>> CreateSplitTransaction([FromBody] CreateSplitTransactionRequest splitTransactionRequest, CancellationToken cancellationToken)
        {
            if (splitTransactionRequest == null)
            {
                return BadRequest("SplitTransaction cannot be null.");
            }

            if (splitTransactionRequest.TotalAmount <= 0)
            {
                return BadRequest("TotalAmount must be greater than zero.");
            }

            if (splitTransactionRequest.Participants == null || splitTransactionRequest.Participants.Count == 0)
            {
                return BadRequest("At least one participant is required.");
            }

            var createdByUser = await _appDbContext.Users
                .FirstOrDefaultAsync(u => u.PublicIdentifier == splitTransactionRequest.CreatedByPublicIdentifier, cancellationToken);
            var paidByUser = await _appDbContext.Users
                .FirstOrDefaultAsync(u => u.PublicIdentifier == splitTransactionRequest.PaidByPublicIdentifier, cancellationToken);

            if (createdByUser == null || paidByUser == null)
            {
                return NotFound("The user specified could not be found.");
            }

            var participants = await _appDbContext.Users
                .Where(u => splitTransactionRequest.Participants.Select(p => p.PublicIdentifier).Contains(u.PublicIdentifier))
                .ToListAsync(cancellationToken);

            var splitTransaction = new SplitTransaction
            {
                Id = Guid.NewGuid(),
                PublicIdentifier = Guid.NewGuid(),
                Title = splitTransactionRequest.Title,
                TotalAmount = splitTransactionRequest.TotalAmount,
                TransactionDateTIme = splitTransactionRequest.TransactionDateTIme,
                SplitDateTIme = DateTime.UtcNow,
                DueDateTime = splitTransactionRequest.DueDateTime,
                PaidBy = paidByUser.PublicIdentifier,
                CreatedBy = createdByUser.PublicIdentifier,
                ParticipantPublicIdentifiers = participants.Select(p => p.PublicIdentifier).ToList()
            };

            _appDbContext.SplitTransactions.Add(splitTransaction);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            var splitAmount = splitTransaction.TotalAmount / participants.Count;

            var createdSplitTransactionResponse = new CreateSplitTransactionResponse
            {
                Id = splitTransaction.Id,
                PublicIdentifier = splitTransaction.PublicIdentifier,
                Title = splitTransaction.Title,
                TotalAmount = splitTransaction.TotalAmount,
                TransactionDateTIme = splitTransaction.TransactionDateTIme,
                SplitDateTIme = splitTransaction.SplitDateTIme,
                DueDateTime = splitTransaction.DueDateTime,
                PaidBy = new CreateUserResponse
                {
                    PublicIdentifier = paidByUser.PublicIdentifier,
                    FirstName = paidByUser.FirstName,
                    LastName = paidByUser.LastName,
                    EmailId = paidByUser.EmailId,
                    ProfilePictureUrl = paidByUser.ProfilePictureUrl
                },
                CreatedBy = new CreateUserResponse
                {
                    PublicIdentifier = createdByUser.PublicIdentifier,
                    FirstName = createdByUser.FirstName,
                    LastName = createdByUser.LastName,
                    EmailId = createdByUser.EmailId,
                    ProfilePictureUrl = createdByUser.ProfilePictureUrl
                },
                Participants = participants.Select(p => new ParticipantDetails
                {
                    PublicIdentifier = p.PublicIdentifier,
                    SplitAmount = splitAmount,
                    AmountPaid = 0,
                    BalanceAmount = splitAmount,
                    SettlementStatus = Enums.SettlementStatus.Pending,
                    Participant = new CreateUserResponse
                    {
                        PublicIdentifier = p.PublicIdentifier,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        EmailId = p.EmailId,
                        ProfilePictureUrl = p.ProfilePictureUrl
                    }
                }).ToList()
            };

            return CreatedAtAction(nameof(GetSplitTransactionById), new { id = createdSplitTransactionResponse.PublicIdentifier }, createdSplitTransactionResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CreateSplitTransactionResponse>> GetSplitTransactionById(Guid id, CancellationToken cancellationToken)
        {
            var splitTransaction = await _appDbContext.SplitTransactions
                .FirstOrDefaultAsync(st => st.PublicIdentifier == id, cancellationToken);

            if (splitTransaction == null)
            {
                return NotFound($"SplitTransaction with PublicIdentifier {id} was not found.");
            }

            var paidByUser = await _appDbContext.Users
                .FirstOrDefaultAsync(u => u.PublicIdentifier == splitTransaction.PaidBy, cancellationToken);

            var createdByUser = await _appDbContext.Users
                .FirstOrDefaultAsync(u => u.PublicIdentifier == splitTransaction.CreatedBy, cancellationToken);

            var participants = await _appDbContext.Users
                .Where(u => splitTransaction.ParticipantPublicIdentifiers.Contains(u.PublicIdentifier))
                .ToListAsync(cancellationToken);

            var splitAmount = splitTransaction.TotalAmount / participants.Count;

            var splitTransactionResponse = new CreateSplitTransactionResponse
            {
                Id = splitTransaction.Id,
                PublicIdentifier = splitTransaction.PublicIdentifier,
                Title = splitTransaction.Title,
                TotalAmount = splitTransaction.TotalAmount,
                TransactionDateTIme = splitTransaction.TransactionDateTIme,
                SplitDateTIme = splitTransaction.SplitDateTIme,
                DueDateTime = splitTransaction.DueDateTime,
                PaidBy = new CreateUserResponse
                {
                    PublicIdentifier = paidByUser.PublicIdentifier,
                    FirstName = paidByUser.FirstName,
                    LastName = paidByUser.LastName,
                    EmailId = paidByUser.EmailId,
                    ProfilePictureUrl = paidByUser.ProfilePictureUrl
                },
                CreatedBy = new CreateUserResponse
                {
                    PublicIdentifier = createdByUser.PublicIdentifier,
                    FirstName = createdByUser.FirstName,
                    LastName = createdByUser.LastName,
                    EmailId = createdByUser.EmailId,
                    ProfilePictureUrl = createdByUser.ProfilePictureUrl
                },
                Participants = participants.Select(p => new ParticipantDetails
                {
                    PublicIdentifier = p.PublicIdentifier,
                    SplitAmount = splitAmount,
                    AmountPaid = 0,
                    BalanceAmount = splitAmount,
                    SettlementStatus = Enums.SettlementStatus.Pending,
                    Participant = new CreateUserResponse
                    {
                        PublicIdentifier = p.PublicIdentifier,
                        FirstName = p.FirstName,
                        LastName = p.LastName,
                        EmailId = p.EmailId,
                        ProfilePictureUrl = p.ProfilePictureUrl
                    }
                }).ToList()
            };

            return Ok(splitTransactionResponse);
        }
    }
}
