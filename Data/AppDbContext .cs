/*
    Date 3 Mar 2026
*/

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using CSLA.Models.Player;

namespace CSLA.Data
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 🔹 Profile (1:1)
            modelBuilder.Entity<ProfileModel>()
                .HasOne(p => p.Player)
                .WithOne(p => p.Profile)
                .HasForeignKey<ProfileModel>(p => p.PlayerId);
            // 🔹 NativeName (1:1)
            modelBuilder.Entity<NativeNameModel>()
                .HasOne(n => n.Player)
                .WithOne(p => p.NativeName)
                .HasForeignKey<NativeNameModel>(n => n.PlayerId);    
            // 🔹 AlternateID (1:N)
            modelBuilder.Entity<AlternateIDModel>()
                .HasOne(a => a.Player)
                .WithMany(p => p.AlternateIDs)
                .HasForeignKey(a => a.PlayerId);

            base.OnModelCreating(modelBuilder);
        }
        public virtual DbSet<PlayerModel> PlayerPlayer { get; set; }
        public virtual DbSet<ProfileModel> PlayerProfile { get; set; }
        public virtual DbSet<NativeNameModel> PlayerNativeName { get; set; }
        public virtual DbSet<AlternateIDModel> PlayerAlternateID { get; set; }
    }
}

