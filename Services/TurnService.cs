using Microsoft.EntityFrameworkCore;
using queue_management.Data;
using queue_management.Models;
using queue_management.Responses;

namespace queue_management.Services;

public class TurnService
{
    private readonly MySqlDbContext _context;

    public TurnService(MySqlDbContext context)
    {
        _context = context;
    }
    
    public ServiceResponse<IEnumerable<Turn>> GetAllTurns()
    {
        var turns = _context.turns
            .Include(t=> t.User)
            .OrderBy(t => t.CreatedAt)
            .ToList();

        return new ServiceResponse<IEnumerable<Turn>>()
        {
            Success = true,
            Data = turns
        };
    }
    
    public ServiceResponse<IEnumerable<Turn>> GetTurnsByStatus(TurnStatus status)
    {
        var turns = _context.turns
            .Include(t => t.User)
            .Where(t => t.Status == status)
            .OrderBy(t => t.CreatedAt)
            .ToList();

        return new ServiceResponse<IEnumerable<Turn>>()
        {
            Success = true,
            Data = turns
        };
    }
    
    public ServiceResponse<IEnumerable<Turn>> GetQueue()
    {
        var turns = _context.turns
            .Include(t => t.User)
            .Where(t => t.Status == TurnStatus.Pending || t.Status == TurnStatus.Waiting || t.Status == TurnStatus.InService)
            .OrderBy(t => t.CreatedAt)
            .ToList();

        return new ServiceResponse<IEnumerable<Turn>>()
        {
            Success = true,
            Data = turns
        };
    }
    
    public ServiceResponse<Turn> SaveTurn(int userId)
    {
        var user = _context.users.Find(userId);

        if (user == null)
        {
            return new ServiceResponse<Turn>()
            {
                Success = false,
                Data = null,
                Message = "User not found. Please register the user first."
            };
        }
        
        var activeTurn = _context.turns.FirstOrDefault(t =>
            t.UserId == userId &&
            t.Status != TurnStatus.Finished);

        if (activeTurn != null)
        {
            return new ServiceResponse<Turn>()
            {
                Success = false,
                Data = null,
                Message = "This user already has an active turn."
            };
        }
        
        int count = _context.turns.Count() + 1;
        string ticketCode = $"A-{count:D3}";

        var turn = new Turn
        {
            TicketCode = ticketCode,
            Status = TurnStatus.Pending,
            UserId = userId,
            CreatedAt = DateTime.Now,
            Comment = BuildTrace("Turn created in pending state.")
        };

        _context.turns.Add(turn);
        _context.SaveChanges();

        return new ServiceResponse<Turn>()
        {
            Success = true,
            Data = turn,
            Message = $"Turn {ticketCode} created successfully"
        };
    }

    public ServiceResponse<Turn> MoveToWaiting(int id)
    {
        var turn = _context.turns.Include(t => t.User).FirstOrDefault(t => t.Id == id);

        if (turn == null)
        {
            return new ServiceResponse<Turn> { Success = false, Message = "Turn not found." };
        }

        if (turn.Status != TurnStatus.Pending)
        {
            return new ServiceResponse<Turn>
            {
                Success = false,
                Message = "Only pending turns can move to waiting."
            };
        }

        turn.Status = TurnStatus.Waiting;
        turn.Comment = AppendTrace(turn.Comment, "Turn moved to waiting room.");
        _context.SaveChanges();

        return new ServiceResponse<Turn>
        {
            Success = true,
            Data = turn,
            Message = $"Turn {turn.TicketCode} moved to waiting."
        };
    }
    
    public ServiceResponse<Turn> CallNext()
    {
        var alreadyInService = _context.turns.FirstOrDefault(t => t.Status == TurnStatus.InService);

        if (alreadyInService != null)
        {
            return new ServiceResponse<Turn>()
            {
                Success = false,
                Data = null,
                Message = "There is already a turn in service. Finish it before calling the next one."
            };
        }
        
        var next = _context.turns
            .Include(t => t.User)
            .Where(t => t.Status == TurnStatus.Waiting)
            .OrderBy(t => t.CreatedAt)
            .FirstOrDefault();

        if (next == null)
        {
            return new ServiceResponse<Turn>()
            {
                Success = false,
                Data = null,
                Message = "No turns waiting in queue."
            };
        }

        next.Status = TurnStatus.InService;
        next.Comment = AppendTrace(next.Comment, "Advisor called turn. Now in service.");
        _context.SaveChanges();

        return new ServiceResponse<Turn>()
        {
            Success = true,
            Data = next,
            Message = $"Now serving turn {next.TicketCode}"
        };
    }
    
    public ServiceResponse<Turn> FinishTurn(int id, string? comment)
    {
        var turn = _context.turns.Find(id);

        if (turn == null)
        {
            return new ServiceResponse<Turn>()
            {
                Success = false,
                Data = null,
                Message = "Turn not found."
            };
        }

        if (turn.Status != TurnStatus.InService)
        {
            return new ServiceResponse<Turn>()
            {
                Success = false,
                Data = null,
                Message = "Only a turn that is currently in service can be finished."
            };
        }

        turn.Status = TurnStatus.Finished;
        var finalNote = string.IsNullOrWhiteSpace(comment) ? "Turn finished by advisor." : $"Turn finished. Note: {comment}";
        turn.Comment = AppendTrace(turn.Comment, finalNote);
        _context.SaveChanges();

        return new ServiceResponse<Turn>()
        {
            Success = true,
            Data = turn,
            Message = "Turn finished successfully."
        };
    }

    public ServiceResponse<Turn> MarkAbsent(int id, string? comment)
    {
        var turn = _context.turns.Find(id);
        if (turn == null)
        {
            return new ServiceResponse<Turn> { Success = false, Message = "Turn not found." };
        }

        if (turn.Status != TurnStatus.InService)
        {
            return new ServiceResponse<Turn> { Success = false, Message = "Only a turn in service can be marked absent." };
        }

        turn.Status = TurnStatus.Finished;
        var note = string.IsNullOrWhiteSpace(comment) ? "Turn marked as absent." : $"Turn marked absent. Note: {comment}";
        turn.Comment = AppendTrace(turn.Comment, note);
        _context.SaveChanges();

        return new ServiceResponse<Turn>
        {
            Success = true,
            Data = turn,
            Message = $"Turn {turn.TicketCode} marked absent."
        };
    }

    private static string BuildTrace(string message)
    {
        return $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
    }

    private static string AppendTrace(string? existing, string message)
    {
        var entry = BuildTrace(message);
        return string.IsNullOrWhiteSpace(existing) ? entry : $"{existing}\n{entry}";
    }
}
