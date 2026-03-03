using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CSTV_v1.Models.Player;

public partial class CSTVContext : DbContext
{
    public CSTVContext()
    {
    }

    public CSTVContext(DbContextOptions<CSTVContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AlternateId> AlternateIds { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AlternateId>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Alternat__3214EC07A0835581");

            entity.HasOne(d => d.Player).WithMany(p => p.AlternateIds)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Alternate__Playe__5CD6CB2B");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Player__3214EC07DC36D9F0");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasMany(d => d.Roles).WithMany(p => p.Players)
                .UsingEntity<Dictionary<string, object>>(
                    "PlayerRole",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PlayerRol__RoleI__6A30C649"),
                    l => l.HasOne<Player>().WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__PlayerRol__Playe__693CA210"),
                    j =>
                    {
                        j.HasKey("PlayerId", "RoleId").HasName("PK__PlayerRo__F2E1D829CAAFEF93");
                        j.ToTable("PlayerRoles", "Player");
                    });
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Profile__3214EC07D87A8FA8");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.Player).WithOne(p => p.Profile)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Profile__PlayerI__5812160E");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC07DC8F5AA3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
