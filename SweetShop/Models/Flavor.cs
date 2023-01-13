using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SweetShop.Models
{
  public class Flavor
  {
    [Required(ErrorMessage = "Flavor must have a name")]
    public string Name { get;set;}
    public int FlavorId {get;set;}
    public List<TreatFlavor> TreatFlavors { get; }
  }
}
// book
  // copies
// author
// authorBooks
// checkout