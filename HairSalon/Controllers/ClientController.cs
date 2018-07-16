using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Salon.Models;

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
          string newName = Request.Form["new-client"];
          int newId = 6;
          //Request.Form["new-id"];
          Client newClient = new Client(newName, newId);
          newClient.Save();
          List<Client> allClients = Client.GetAll();
          return View("Clients", allClients);

        }

      }
    }
