using System.ComponentModel.DataAnnotations;

namespace MyVinCafe.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public string Username { get; set; } = "";
        public string Password { get; set; } = ""; // Nanti simpan password di sini
        public string Role { get; set; } = "Customer"; // Admin, Kasir, Owner, Supervisor, Customer
    }
}