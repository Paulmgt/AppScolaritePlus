using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppScolaritePlus.Models
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Modules> Modules { get; set; }

        public DbSet<Parcours> Parcours { get; set; }

        public DbSet<Utilisateurs> Utilisateurs { get; set; }

        public DbSet<SessionsFormation> SessionsFormations { get; set; }

        public DbSet<UnitesPedagogique> UnitesPedagogiques { get; set; }

    }
}