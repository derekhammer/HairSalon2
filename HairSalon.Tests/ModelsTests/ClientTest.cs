using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salon.Models;
using System;
using System.Collections.Generic;

namespace Salon.Tests
{
  [TestClass]
  public class ClientTests
  {
  [TestMethod]
  public void Save_SaveName_True()
    {
      string name = "Cody";
      int ID = 1;
      Client test = new Client(name, ID);
      string result = test.GetName();

      Assert.AreEqual(name, result);
    }
    [TestMethod]
    public void Save_SaveID_True()
      {
        string name = "Cody";
        int ID = 1;
        Client test = new Client(name, ID);
        int result = test.GetId();

        Assert.AreEqual(ID, result);
      }
      [TestMethod]
      public void 
      {

      }
  }
}
