using Microsoft.EntityFrameworkCore;

namespace LaboratoriumASPNET.Models;

public class AppDbContext : DbContext
{
    public DbSet<ContactEntity> Contacts { 
        get; 
        set;
    }
    
    public DbSet<OrganizationEntity> Organizations { get; set; }
    private string DbPath { get; set; }
    public AppDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "contacts.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data source = {DbPath}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<OrganizationEntity>()
            .OwnsOne(o => o.Address)
            .HasData(
                new {OrganizationEntityId = 1, City = "Kraków", Street = "Św. Filipa 17"},
                new {OrganizationEntityId = 2, City = "Warszawa", Street = "Wesoła 15"}
            );

       modelBuilder.Entity<ContactEntity>()
           .HasOne<OrganizationEntity>(c => c.Organization)
           .WithMany(o => o.Contacts)
           .HasForeignKey(c => c.OrganizationId);

       modelBuilder.Entity<OrganizationEntity>()
           .HasData(
                new OrganizationEntity
                {
                    Id = 1,
                    Regon = "321321321",
                    Nip = "123456",
                    Name = "WSEI",
                },
                
                new OrganizationEntity
                {
                    Id = 2,
                    Regon = "123123123",
                    Nip = "432432",
                    Name = "Famo",   
                }
                
           );
        
        modelBuilder.Entity<ContactEntity>()
            .HasData(
                new ContactEntity()
                {
                    Id = 1,
                    FirstName = "Adam",
                    LastName = "Nowak",
                    PhoneNumber = "123123123",
                    BirthDate = new DateTime(1980, 1, 1),
                    Email = "ewa@wsei.edu.pl",
                    Created = DateTime.Now,
                    OrganizationId = 1
                },
                
                new ContactEntity()
                {
                    Id = 2,
                    FirstName = "Ola",
                    LastName = "Nowak",
                    PhoneNumber = "123123123",
                    BirthDate = new DateTime(2001, 1, 1),
                    Email = "ola@wsei.edu.pl",
                    Created = DateTime.Now,
                    OrganizationId = 2
                }
            );
    }
}