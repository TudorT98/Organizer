using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Organizer.Model
{
    public class OrganizerDbContext:DbContext
    {
    
        public OrganizerDbContext(DbContextOptions<OrganizerDbContext> options ) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => new { u.FirstName, u.LasttName }).IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => new { u.UserName }).IsUnique();
            modelBuilder.Entity<Calendar>()
                .HasIndex(c => new { c.day, c.UserId }).IsUnique();
            modelBuilder.Entity<Calendar>()
                .HasIndex(c => c.UserId).IsUnique();
            //modelBuilder.Entity<Calendar>()
            //.HasOne<User>(u => u.user)
            //.WithMany(c => c.program);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Calendar> Bookings { get; set; }
    }
}
