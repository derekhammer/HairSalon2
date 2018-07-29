using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salon.Models;
using System;
using System.Collections.Generic;

namespace Salon.Tests
{
  [TestClass]
  public class StylistTests
  {
  [TestMethod]
    public void Save_SaveName_True()
    {

      string name = "Derek";
      string details = "Does Hair";
      Stylist test = new Stylist(name, details);
      string result = test.GetName();

      Assert.AreEqual(name, result);
    }
    [TestMethod]
      public void Save_SaveDetails_True()
      {

        string name = "Derek";
        string details = "Does Hair";
        Stylist test = new Stylist(name, details);
        string result = test.GetDetail();

        Assert.AreEqual(details, result);
      }
  }
}
