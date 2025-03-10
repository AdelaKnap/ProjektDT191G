using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjektDT191G.Models;

namespace ProjektDT191G.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // Tabeller
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Lecture>? Lectures { get; set; }
    public DbSet<QuoteRequest>? QuoteRequests { get; set; }
    public DbSet<Speaker>? Speakers { get; set; }

}
