using Microsoft.EntityFrameworkCore;

namespace BekendDeo.Models
{
    public class HotelContext : DbContext
    {
        public DbSet<Korisnik> Korisnici {get;set;}
        public DbSet<Musterija> Musterije { get; set; }
        public DbSet<Recenzija> Recenzije { get; set; }
        public DbSet<Pitanje> Pitanja { get; set; }
        public DbSet<Radnik> Radnici { get; set; }
        public DbSet<Zahtev> Zahtevi { get; set; }
        public DbSet<Obavestenje> Obavestenja { get; set; }
        public DbSet<Administrator> Admin { get; set; }
        public DbSet<Usluga> Usluge { get; set; }
        public DbSet<ZahtevUsluga> Sastojci { get; set; }

        public HotelContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ZahtevUsluga>().HasKey(ck =>  new 
            {
                ck.ZahtevID, ck.UslugaID
            });
            modelBuilder.Entity<Usluga>().HasIndex(u => u.Naziv).IsUnique();
            modelBuilder.Entity<Radnik>().HasIndex(r => r.JMBG).IsUnique();
            modelBuilder.Entity<Korisnik>().HasIndex(k => k.Username).IsUnique();
            modelBuilder.Entity<Radnik>().HasIndex(k => k.Username).IsUnique();
            modelBuilder.Entity<Administrator>().HasIndex(k => k.Username).IsUnique();
            modelBuilder.Entity<Musterija>().HasIndex(k => k.Username).IsUnique();

    
        }
    }
}