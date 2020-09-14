using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {

        public ApplicationDbContext(
            DbContextOptions options) : base(options)
        {
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            int result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        public DbSet<User> Users { get; set; }
    }
}