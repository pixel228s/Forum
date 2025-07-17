using Forum.Domain.Models.Base;
using Forum.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Forum.Persistence.Data
{
    public class ForumDbContext : DbContext
    {
        public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
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
                if (entity.State == EntityState.Modified)
                {
                    entity.Entity.CreatedAt = DateTime.UtcNow;
                }

                entity.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
