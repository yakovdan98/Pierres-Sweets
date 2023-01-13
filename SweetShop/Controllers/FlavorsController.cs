using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace SweetShop.Models
{
  public class FlavorsController : Controller
  {
    private readonly SweetShopContext _db;
    public FlavorsController(SweetShopContext db)
    {
      _db = db;
    }
    [AllowAnonymous]
    public ActionResult Index()
    {
      return View(_db.Flavors.ToList());
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Flavor flavor)
    {
      if (!ModelState.IsValid)
      {
        return View(flavor);
      }
      else
      {
        _db.Flavors.Add(flavor);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
      Flavor thisFlavor = _db.Flavors
      .Include(joinEntry => joinEntry.TreatFlavors)
      .ThenInclude(entity => entity.Treat)
      .FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }
    [HttpPost]
    public ActionResult Delete(int id)
    {
      Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      _db.Flavors.Remove(thisFlavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult AddTreat(Flavor flavor, int treatId)
    {

#nullable enable
      TreatFlavor? entry = _db.TreatFlavors.FirstOrDefault(entry => (entry.FlavorId == flavor.FlavorId && entry.TreatId == treatId));
#nullable disable
      if (entry == null && treatId != 0)
      {
        _db.TreatFlavors.Add(new TreatFlavor { FlavorId = flavor.FlavorId, TreatId = treatId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", "Flavors", new { id = flavor.FlavorId });
    }

    [HttpPost]
    public ActionResult RemoveJoin(int joinId, int type)
    {
      TreatFlavor joinEntry = _db.TreatFlavors.FirstOrDefault(entry => entry.TreatFlavorId == joinId);
      _db.TreatFlavors.Remove(joinEntry);
      _db.SaveChanges();
      switch (type)
      {
        case 0:
          return RedirectToAction("Details", new { id = joinEntry.FlavorId });
        case 1:
          return RedirectToAction("Details", "Treats", new { id = joinEntry.TreatId });
        default:
          return RedirectToAction("Index");
      }

    }

    public ActionResult Edit(int id)
    {
      Flavor thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }

    [HttpPost]
    public ActionResult Edit(Flavor flavor)
    {
      _db.Flavors.Update(flavor);
      _db.SaveChanges();
      return RedirectToAction("Details", "Flavors",  new { id = flavor.FlavorId});
    }

  }
}