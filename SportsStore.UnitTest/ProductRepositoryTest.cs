using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit;
using NUnit.Framework;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;

namespace SportsStore.UnitTest
{
   [TestFixture]
   public class ProductRepositoryTest
    {
       [Test]
       public void Can_paginate()
       {
           Mock<IProductRepository> mock = new Mock<IProductRepository>();

           mock.Setup(m => m.Products).Returns(new Product[] { 
               new Product{ ProductID = 1, Name = "P1"},
               new Product{ ProductID = 2, Name = "P2"},
               new Product{ ProductID = 3, Name = "P3"},
               new Product{ ProductID = 4, Name = "P4"},
               new Product{ ProductID = 5, Name = "P5"}}.AsQueryable());

           ProductController controller = new ProductController(mock.Object);

           IEnumerable<Product> result = controller.List(2).Model as IEnumerable<Product>;


           Product[] ProductArray = result.ToArray();
           Assert.IsTrue(ProductArray.Length == 2);
           Assert.AreEqual(ProductArray[0].Name, "P4");
           Assert.AreEqual(ProductArray[1].Name, "P5");
               

       }
    }
}
