using System.ComponentModel.DataAnnotations;

namespace MyVinCafe.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        // Tambahkan Role: "Admin" atau "Member" atau lainnya
        public string Role { get; set; } = "Member"; //defaultnya, kalo mao punya role selain member kerja dulu di cafe my /(-3-)/
    }
}