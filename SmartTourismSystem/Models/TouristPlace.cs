using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SmartTourismSystem.Models;

public partial class TouristPlace
{
    [Key]
    public int PlaceId { get; set; }

    [StringLength(200)]
    public string PlaceName { get; set; } = null!;

    [StringLength(1000)]
    public string? Description { get; set; }

    public int? CategoryId { get; set; }

    [StringLength(100)]
    public string? City { get; set; }

    [StringLength(500)]
    public string? Address { get; set; }

    [Column(TypeName = "decimal(10, 8)")]
    public decimal? Latitude { get; set; }

    [Column(TypeName = "decimal(11, 8)")]
    public decimal? Longitude { get; set; }

    [StringLength(50)]
    public string? PriceRange { get; set; }

    [StringLength(100)]
    public string? BestSeason { get; set; }

    [Column(TypeName = "decimal(3, 2)")]
    public decimal? AverageRating { get; set; }

    public DateTime? CreatedAt { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("TouristPlaces")]
    public virtual Category? Category { get; set; }

    [InverseProperty("Place")]
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    [InverseProperty("Place")]
    public virtual ICollection<SmartSuggestion> SmartSuggestions { get; set; } = new List<SmartSuggestion>();
}
