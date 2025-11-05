using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartTourismSystem.Models;

public partial class SmartSuggestion
{
    [Key]
    public int SuggestionId { get; set; }

    public int? UserId { get; set; }

    public int? PlaceId { get; set; }

    [StringLength(500)]
    public string? Reason { get; set; }

    [Column(TypeName = "decimal(3, 2)")]
    public decimal? ConfidenceScore { get; set; }

    public DateTime? SuggestedAt { get; set; }

    public bool? IsViewed { get; set; }

    [ForeignKey("PlaceId")]
    [InverseProperty("SmartSuggestions")]
    public virtual TouristPlace? Place { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("SmartSuggestions")]
    public virtual User? User { get; set; }
}
