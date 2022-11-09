namespace _3dPrinter.Domain.Entity
{
    public class Filament
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer{get; set; }
        public int TempOfPrint { get; set; }
        public decimal Price { get; set; }

    }
    
}
