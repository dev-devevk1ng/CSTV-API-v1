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
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProfileModel>()
                .HasOne(p => p.Player)
                .WithMany()
                .HasForeignKey(p => p.PlayerId)
                .HasPrincipalKey(p => p.Id);
        }
        */
        public virtual DbSet<PlayerModel> PlayerPlayer { get; set; }
        public virtual DbSet<ProfileModel> PlayerProfile { get; set; }
        
    }
}

