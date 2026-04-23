using System.ComponentModel.DataAnnotations;

namespace queue_management.Models;

public class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Document number is required")]
    public string DocumentNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Full name is required")]
    public string FullName { get; set; } = string.Empty;
}