using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CanvasCommunity.Context;

public class UsersContext : IdentityUserContext<IdentityUser>
{
    public UsersContext ( DbContextOptions<UsersContext> options) : base(options){}

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}