using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Legacysql.Models;

public partial class SupportTicketsDbContext : DbContext
{
    public SupportTicketsDbContext()
    {
    }

    public SupportTicketsDbContext(DbContextOptions<SupportTicketsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SupportTicketsDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tickets__3214EC07C2A0E326");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
