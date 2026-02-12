namespace MyVinCafe.Models
{
    public class Keranjang
    {
        public int MenuId { get; set; }
        public string NamaMenu { get; set; } = string.Empty;
        public int Harga { get; set; }
        public int Jumlah { get; set; }
        public int Total => Harga * Jumlah;
    }
}
