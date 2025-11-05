using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SmartTourismSystem.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aichat> Aichats { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<SmartSuggestion> SmartSuggestions { get; set; }

    public virtual DbSet<TouristPlace> TouristPlaces { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=SmartTourismDB;Trusted_Connection=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aichat>(entity =>
        {
            entity.HasKey(e => e.ChatId).HasName("PK__AIChats__A9FBE7C631716211");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.Aichats).HasConstraintName("FK__AIChats__UserId__48CFD27E");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0B3C237679");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79CEFCC07F9D");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Place).WithMany(p => p.Reviews).HasConstraintName("FK__Reviews__PlaceId__440B1D61");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews).HasConstraintName("FK__Reviews__UserId__4316F928");
        });

        modelBuilder.Entity<SmartSuggestion>(entity =>
        {
            entity.HasKey(e => e.SuggestionId).HasName("PK__SmartSug__94099508147DD5EE");

            entity.Property(e => e.IsViewed).HasDefaultValue(false);
            entity.Property(e => e.SuggestedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Place).WithMany(p => p.SmartSuggestions).HasConstraintName("FK__SmartSugg__Place__4D94879B");

            entity.HasOne(d => d.User).WithMany(p => p.SmartSuggestions).HasConstraintName("FK__SmartSugg__UserI__4CA06362");
        });

        modelBuilder.Entity<TouristPlace>(entity =>
        {
            entity.HasKey(e => e.PlaceId).HasName("PK__TouristP__D5222B6EC1B31321");

            entity.Property(e => e.AverageRating).HasDefaultValue(0m);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Category).WithMany(p => p.TouristPlaces).HasConstraintName("FK__TouristPl__Categ__3E52440B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CD8EC9397");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
