using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartTourismSystem.Models;

[Index("Username", Name = "UQ__Users__536C85E4CCA1F8AC", IsUnique = true)]
[Index("Email", Name = "UQ__Users__A9D1053430376B97", IsUnique = true)]
public partial class User
{
    [Key]
    public int UserId { get; set; }

    [StringLength(50)]
    public string Username { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(255)]
    public string PasswordHash { get; set; } = null!;

    [StringLength(50)]
    public string? FirstName { get; set; }

    [StringLength(50)]
    public string? LastName { get; set; }

    public int? Age { get; set; }

    [StringLength(500)]
    public string? Interests { get; set; }

    public DateTime? CreatedAt { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<Aichat> Aichats { get; set; } = new List<Aichat>();

    [InverseProperty("User")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [InverseProperty("User")]
    public virtual ICollection<SmartSuggestion> SmartSuggestions { get; set; } = new List<SmartSuggestion>();
}
