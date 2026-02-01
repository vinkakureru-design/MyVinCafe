namespace MyVinCafe.Models
{
    public class MenuCafe
    {
        public int Id { get; set; }
        
        public string NamaMenu { get; set; } = string.Empty;
        public string Deskripsi { get; set; } = string.Empty;
        public decimal Harga { get; set; }
        public string Kategori { get; set; } = string.Empty;
        public int Diskon { get; set; }

    }
}
