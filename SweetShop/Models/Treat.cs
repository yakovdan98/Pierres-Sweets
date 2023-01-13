using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace SweetShop.Models
{
  public class Treat
  {
    [Required(ErrorMessage = "Treat must have a name")]
    public string Name {get;set;}
    public int TreatId {get;set;}
    public List<TreatFlavor> TreatFlavors { get; }
  }
}
// book
  // copies
// author
// authorBooks
// checkout