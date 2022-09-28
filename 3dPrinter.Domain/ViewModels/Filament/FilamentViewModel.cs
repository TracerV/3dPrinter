using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dPrinter.Domain.ViewModels.Filament
{
    public class FilamentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int TempOfPrint { get; set; }
        public decimal Price { get; set; }
    }
}
