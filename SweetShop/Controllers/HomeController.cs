using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using SweetShop.Models;

namespace SweetShop.Controllers
{
  public class HomeController : Controller
  {
    private readonly SweetShopContext _db;

    public HomeController(SweetShopContext db)
    {
      _db = db;
    }
    // Routes
    [HttpGet("/")]
    public ActionResult Index()
    {
      Flavor[] flavors = _db.Flavors.ToArray();
      Treat[] treats = _db.Treats.ToArray();
      Dictionary<string, object[]> model = new Dictionary<string, object[]>();
      model.Add("flavors", flavors);
      model.Add("treats", treats);
      return View(model);
    }
    // [HttpPost("")]

  }
}