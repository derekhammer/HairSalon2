using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Salon.Models;

namespace Salon.Controllers
{
    public class StylistController : Controller
    {

        [HttpGet("/stylist")]
        public ActionResult Stylists()
        {
          List<Stylist> allStylists = Stylist.GetAll();
          return View(allStylists);
        }

        [HttpGet("/stylist/new")]
        public ActionResult AddStylists()
        {
          Stylist newStylist = new Stylist(Request.Form["new-stylist"], Request.Form["new-details"]);
          newStylist.Save();
          List<Stylist> allStylists = Stylist.GetAll();
          return View("Index", allStylists);
        }
      }
    }
