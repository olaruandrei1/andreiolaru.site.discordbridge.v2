using andreiolaru.site.discord.bridge.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace andreiolaru.site.discord.bridge.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<InboxMessage> Messages => Set<InboxMessage>();
}
