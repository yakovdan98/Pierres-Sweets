using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SweetShop.Models;

namespace SweetShop.Models
{
  public class TreatFlavor
  {
    public int TreatFlavorId {get;set;}
    public int TreatId {get;set;}
    public Treat Treat {get;set;}
    public int FlavorId {get;set;}
    public Flavor Flavor {get;set;}
  }
}
// book
  // copies
// author
// authorBooks
// checkout