using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartTourismSystem.Models;

[Table("AIChats")]
public partial class Aichat
{
    [Key]
    public int ChatId { get; set; }

    public int? UserId { get; set; }

    public string UserMessage { get; set; } = null!;

    [Column("AIResponse")]
    public string? Airesponse { get; set; }

    public string? Category { get; set; }

    public DateTime? CreatedAt { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Aichats")]
    public virtual User? User { get; set; }
}
