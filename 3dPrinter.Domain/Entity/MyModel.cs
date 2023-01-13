using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace _3dPrinter.Domain.Entity;

public class MyModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Cost { get; set; }
    
    public int? Weight { get; set; }
    
    [ForeignKey("Filament")]
    public int? FilamentId { get; set; }
    public virtual Filament Filament { get; set; }
    [ForeignKey("Customer")]
    public int? CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    public TimeSpan TimeOfPrint { get ; set; }
    
}