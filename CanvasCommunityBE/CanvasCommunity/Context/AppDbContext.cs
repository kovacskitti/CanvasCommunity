using Microsoft.EntityFrameworkCore;

namespace CanvasCommunity.Context;

public class AppDbContext : DbContext
{
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Painting> Paintings { get; set; }
    private readonly IConfiguration _configuration;

    public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration ) : base(options)
    {
        _configuration = configuration;
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
    }
}
