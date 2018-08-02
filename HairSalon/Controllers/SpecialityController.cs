using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Salon.Models;
using System;

namespace Salon.Controllers
{
    public class SpecialityController : Controller
    {
      [HttpGet("/speciality")]
      public ActionResult Speciality()
      {
        return View(Speciality.GetAll());
      }
      [HttpGet ("/speciality/add")]
      public ActionResult AddSpeciality()
      {
        return View();
      }
      [HttpPost("/speciality")]
      public ActionResult AddNewSpeciality()
      {
        Speciality newSpeciality = new Speciality(Request.Form["new-speciality"]);
        newSpeciality.Save();
        return RedirectToAction ("Speciality");
      }
    }
  }
