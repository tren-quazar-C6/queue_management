using System.ComponentModel.DataAnnotations;

namespace queue_management.Models;

public class Turn
{
    public int Id { get; set; }
    
    public string TicketCode { get; set; } = string.Empty;
    
    public TurnStatus Status { get; set; } = TurnStatus.Waiting;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    public string? Comment { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
    public User? User { get; set; }
}