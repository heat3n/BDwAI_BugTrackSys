using BDwAI_BugTrackSys.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BDwAI_BugTrackSys.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Powiadomienie> Powiadomienia { get; set; }
        public DbSet<Projekt> Projekty { get; set; }
        public DbSet<Priorytet> Priorytety { get; set; }
        public DbSet<Status> Statusy { get; set; }
        public DbSet<Zgloszenie> Zgloszenia { get; set; }
    }
}