using Forum.Domain.Models;
using Forum.Domain.Models.Base;
using Forum.Domain.Models.Posts;
using Forum.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Forum.Persistence.Data
{
    public class ForumDbContext : IdentityDbContext<User, Role, int>
    {
        public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Ignore<IdentityUserToken<int>>();
            modelBuilder.Ignore<IdentityUserLogin<int>>();
            modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ForumDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            EntityPreparations();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void EntityPreparations()
        {
            var entities = ChangeTracker
                .Entries<BaseEntity>()
                .Where(c => c.State == EntityState.Modified || c.State == EntityState.Added);

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedAt = DateTime.UtcNow;
                }

                entity.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
