using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Salon.Models;
using System;

namespace Salon.Controllers
{
    public class ClientController : Controller
    {

        [HttpGet("/client")]
        public ActionResult Clients()
        {
          //List<Stylist> allStylists = Stylist.GetAll();
          return View(Client.GetAll());
        }
        [HttpGet ("/client/add")]
        public ActionResult AddClient()
        {
          return View();
        }
        [HttpPost("/client")]
        public ActionResult AddNewClient()
        {
          Client newClient = new Client(Request.Form["new-client"],  int.Parse(Request.Form["stylist-Id"]));
          newClient.Save();
          return RedirectToAction ("Clients");
        }

      }
    }
