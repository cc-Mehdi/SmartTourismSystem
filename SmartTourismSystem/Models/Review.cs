using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartTourismSystem.Models;

public partial class Review
{
    [Key]
    public int ReviewId { get; set; }

    public int? UserId { get; set; }

    public int? PlaceId { get; set; }

    public int? Rating { get; set; }

    [StringLength(1000)]
    public string? Comment { get; set; }

    public DateTime? CreatedAt { get; set; }

    [ForeignKey("PlaceId")]
    [InverseProperty("Reviews")]
    public virtual TouristPlace? Place { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("Reviews")]
    public virtual User? User { get; set; }
}
