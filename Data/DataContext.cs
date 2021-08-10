using Laborlance_API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Laborlance_API.Data
{
    public class DataContext : IdentityDbContext<User, Role, int, 
        IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){ }
        public DbSet<Tool> Tools { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Renter> Renters { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Proposal> Proposals { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ToolInProposal> ToolsInProposals { get; set; }
        public DbSet<ToolImage> ToolImages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserRole>(userRole => {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });
                userRole.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();    
                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.OrderedOperations)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Worker>()
                .HasMany(w => w.Proposals)
                .WithOne(p => p.Worker)
                .HasForeignKey(p => p.WorkerId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<Worker>()
                .HasMany(w => w.Operations)
                .WithOne(o => o.Worker)
                .HasForeignKey(o => o.WorkerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Renter>()
                .HasMany(r => r.Tools)
                .WithOne(t => t.Renter)
                .HasForeignKey(t => t.RenterId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Proposals)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Operation>()
                .HasMany(o => o.ToolsInRent)
                .WithOne(t => t.Operation)
                .HasForeignKey(t => t.OperationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Worker>()
                .HasMany(w => w.ReceivedReviews)
                .WithOne(r => r.Worker)
                .HasForeignKey(r => r.WorkerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.GivenReviews)
                .WithOne(r => r.Customer)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Proposal>()
                .HasMany(p => p.ProposedTools)
                .WithOne(tip => tip.Proposal)
                .HasForeignKey(tip => tip.ProposalId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Tool>()
                .HasMany(t => t.ToolInProposal)
                .WithOne(tip => tip.Tool)
                .HasForeignKey(tip => tip.ToolId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}