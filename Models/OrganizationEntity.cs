using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    [Table("organizations")]
    public class OrganizationEntity
    {
        public int Id { get; set; }  // Klucz główny
        public string Name { get; set; }
        public string Regon { get; set; }
        public string Nip { get; set; }

        // Zagnieżdżona encja Address
        public Address? Address { get; set; }

        // Zbiór powiązanych encji ContactEntity
        public ISet<ContactEntity> Contacts { get; set; }
    }

    // Klasa Address, która jest częścią OrganizationEntity
    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
    }
}