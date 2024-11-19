using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Project.Models
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<ContactEntity> Contacts { get; set; }
        public DbSet<OrganizationEntity> Organizations { get; set; }

        private string DbPath { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Ścieżka do bazy danych SQLite
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "contacts.db");
            optionsBuilder.UseSqlite($"Data source={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Konfiguracja Address jako Owned Entity w OrganizationEntity
            modelBuilder.Entity<OrganizationEntity>()
                .OwnsOne(o => o.Address, a =>
                {
                    a.Property(p => p.City).HasMaxLength(100);  // Dodatkowe właściwości, np. maksymalna długość stringa
                    a.Property(p => p.Street).HasMaxLength(200);
                });

            // Dalsza konfiguracja modelu
            string ADMIN_ID = Guid.NewGuid().ToString();
            string USER_ID = Guid.NewGuid().ToString();

            // Dodawanie ról
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = ADMIN_ID, Name = "admin", NormalizedName = "ADMIN", ConcurrencyStamp = ADMIN_ID },
                new IdentityRole() { Id = USER_ID, Name = "user", NormalizedName = "USER", ConcurrencyStamp = USER_ID }
            );

            var admin = new IdentityUser()
            {
                Id = ADMIN_ID,
                UserName = "Adam",
                NormalizedUserName = "ADAM",
                Email = "adam@wsei.edu.pl",
                NormalizedEmail = "ADAM@WSEI.EDU.PL",
                EmailConfirmed = true
            };
            var user = new IdentityUser()
            {
                Id = USER_ID,
                UserName = "Kuba",
                NormalizedUserName = "KUBA",
                Email = "kuba@wsei.edu.pl",
                NormalizedEmail = "KUBA@WSEI.EDU.PL",
                EmailConfirmed = true
            };

            // Haszowanie haseł
            PasswordHasher<IdentityUser> hasher = new PasswordHasher<IdentityUser>();
            admin.PasswordHash = hasher.HashPassword(admin, "1234!");
            user.PasswordHash = hasher.HashPassword(user, "1234@");

            // Dodawanie użytkowników
            modelBuilder.Entity<IdentityUser>().HasData(admin, user);

            // Powiązanie użytkowników z rolami
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = ADMIN_ID, UserId = ADMIN_ID },
                new IdentityUserRole<string> { RoleId = USER_ID, UserId = USER_ID }
            );

            // Dodawanie organizacji
            modelBuilder.Entity<OrganizationEntity>().HasData(
                new OrganizationEntity() { Id = 1, Name = "WSEI", Regon = "22223333", Nip = "123123123" },
                new OrganizationEntity() { Id = 2, Name = "Webcon", Regon = "4443331", Nip = "1212121" }
            );

            // Dodawanie kontaktów
            modelBuilder.Entity<ContactEntity>().HasData(
                new ContactEntity()
                {
                    Id = 1,
                    FirstName = "Marian",
                    LastName = "Kowalski",
                    BirthDate = new DateTime(2000, 10, 10),
                    PhoneNumber = "333 333 333",
                    Email = "mariankowalski@wsei.edu.pl",
                    Created = DateTime.Now,
                    OrganizationId = 1
                },
                new ContactEntity()
                {
                    Id = 2,
                    FirstName = "Jakub",
                    LastName = "Nowak",
                    BirthDate = new DateTime(2000, 11, 10),
                    PhoneNumber = "111 111 111",
                    Email = "jn@wsei.edu.pl",
                    Created = DateTime.Now,
                    OrganizationId = 2
                }
            );
        }
    }
}
