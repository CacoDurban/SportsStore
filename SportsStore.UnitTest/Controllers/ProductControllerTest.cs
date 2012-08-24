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
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTest
{
   [TestFixture]
   public class ProductControllerTest
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

          var result = controller.List(null, 2).Model as ProductListViewModel;


           Product[] ProductArray = result.Products.ToArray();
           Assert.IsTrue(ProductArray.Length == 2);
           Assert.AreEqual(ProductArray[0].Name, "P4");
           Assert.AreEqual(ProductArray[1].Name, "P5");
               

       }

       [Test]
       public void Can_paginate_with_Category()
       {
           Mock<IProductRepository> mock = new Mock<IProductRepository>();

           mock.Setup(m => m.Products).Returns(new Product[] { 
               new Product{ ProductID = 1, Name = "P1", Category = "c1"},
               new Product{ ProductID = 2, Name = "P2", Category = "c1"},
               new Product{ ProductID = 3, Name = "P3", Category = "c2"},
               new Product{ ProductID = 4, Name = "P4", Category = "c3"},
               new Product{ ProductID = 5, Name = "P5"}}.AsQueryable());

           ProductController controller = new ProductController(mock.Object);

           var result = controller.List("c2").Model as ProductListViewModel;


           Product[] ProductArray = result.Products.ToArray();
           Assert.IsTrue(ProductArray.Length == 1);
           Assert.AreEqual(ProductArray[0].Name, "P3");
           


       }


       [Test]
       public void GetProducts_add_the_product_to_lasted_viewed_collection()
       {
           
       }


    }
}
