using Microsoft.EntityFrameworkCore;
using MielMasse.Models.Domain;

namespace MielMasse.Data
{
    public class MielMasseDbContext : DbContext
    {
        public MielMasseDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Utilisateur> Utilisateurs { get; set; }
    }
}
