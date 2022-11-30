using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace _3dPrinter.Domain.ViewModels.MyModel;

public class MyModelViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Cost { get; set; }
    
    [ForeignKey("Filament")]
    public int? FilamentId { get; set; }
    public virtual Entity.Filament Filament { get; set; }
    [ForeignKey("Customer")]
    public int? CustomerId { get; set; }
    public virtual Entity.Customer Customer { get; set; }
    public DateTime TimeOfPrint { get; set; }
}