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
          //List<Stylist> allStylists = Stylist.GetAll();
          return View(Stylist.GetAll());
        }
        [HttpGet ("/stylist/add")]
        public ActionResult AddStylist()
        {
          return View();
        }
        [HttpPost("/stylist")]
        public ActionResult AddNewStylist()
        {
          string newName = Request.Form["new-stylist"];
          string newDetails = Request.Form["new-details"];
          Stylist newStylist = new Stylist(newName, newDetails);
          newStylist.Save();
          List<Stylist> allStylists = Stylist.GetAll();
          return View("Stylists", allStylists);

        }
        [HttpPost("/stylist/delete")]
        public ActionResult DeleteAll()
        {
          Stylist.DeleteAll();
          return View();
        }
        [HttpGet("/stylist/{id}")]
        public ActionResult StylistList(int id)
        {
          Dictionary<string, object> model = new Dictionary<string, object>();
          Stylist selectedStylist = Stylist.Find(id);
          List<Client> selectedClients = Client.GetClientId(id);
          List<Client> allClients = Client.GetAll();
          List<Speciality> getSpeciality = Speciality.Find(id);
          model.Add("getSpeciality", getSpeciality);
          model.Add("selectedClients", selectedClients);
          model.Add("allClients", allClients);
          return View(model);
        }
        [HttpGet ("/stylist/{id}/editstylist")]
        public ActionResult EditStylist(int Id)
        {
          Stylist thisStylist = Stylist.Find(Id);
          return View(thisStylist);
        }
        [HttpPost ("/stylist/{id}/editstylist")]
        public ActionResult Edit(int Id)
        {
          Stylist thisStylist = Stylist.Find(Id);
          thisStylist.EditStylist(Request.Form["editStylist"]);
          return RedirectToAction("Stylists");
        }
      }
    }
