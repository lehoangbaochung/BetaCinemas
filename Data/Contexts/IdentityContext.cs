using BetaCinemas.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BetaCinemas.Migrations
{
    public class IdentityContext : IdentityDbContext<Member>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();

                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName[6..]);
                }
            }
        }
    }
}
