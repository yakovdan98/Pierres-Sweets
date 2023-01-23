using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using SweetShop.Models;
using Microsoft.AspNetCore.Authorization;


namespace SweetShop.Controllers
{
  [Authorize]
  public class TreatsController : Controller
  {
    // Routes
    private readonly SweetShopContext _db;
    public TreatsController(SweetShopContext db)
    {
      _db = db;
    }
    [AllowAnonymous]
    public ActionResult Index()
    {
      List<Treat> treats = _db.Treats.ToList();
      return View(treats);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Treat treat)
    {
      if (!ModelState.IsValid)
      {
        return View(treat);
      }
      else
      {
        _db.Treats.Add(treat);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }

    }
    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
      Treat thisTreat = _db.Treats
        .Include(joinEntry => joinEntry.TreatFlavors)
        .ThenInclude(entity => entity.Flavor)
        .FirstOrDefault(treat => treat.TreatId == id);
      return View(thisTreat);
    }

    public ActionResult Edit(int id)
    {
      Treat thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult Edit(Treat treat)
    {
      if (!ModelState.IsValid)
      {
        return View(treat);
      }
      else
      {
        _db.Treats.Update(treat);
        _db.SaveChanges();
        return RedirectToAction("Details", "Treats", new { id = treat.TreatId });
      }
    }


    [HttpPost]
    public ActionResult AddFlavor(Treat treat, int flavorId)
    {
#nullable enable
      TreatFlavor? entry = _db.TreatFlavors.FirstOrDefault(entry => (entry.TreatId == treat.TreatId && entry.FlavorId == flavorId));
#nullable disable
      if (entry == null && flavorId != 0)
      {
        _db.TreatFlavors.Add(new TreatFlavor { TreatId = treat.TreatId, FlavorId = flavorId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", "Treats", new { id = treat.TreatId });
    }

    [HttpPost]
    public ActionResult Delete(int id)
    {
      Treat thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      _db.Treats.Remove(thisTreat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}