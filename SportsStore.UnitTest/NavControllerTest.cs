using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SportsStore.WebUI.Controllers;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.UnitTest
{
    [TestFixture]
    public class NavControllerTest
    {
        [Test]
        public void Generate_Category_List()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            
            mock.Setup(m => m.Products).Returns(new Product[] { 
               new Product{ ProductID = 1, Name = "P1", Category = "c2"},
               new Product{ ProductID = 2, Name = "P2", Category = "c2"},
               new Product{ ProductID = 3, Name = "P3", Category = "c3"},
               new Product{ ProductID = 4, Name = "P4", Category = "c4"},
               new Product{ ProductID = 5, Name = "P5", Category = "c5"}}.AsQueryable());

            NavController controller = new NavController(mock.Object);

            string[] categories = ((IEnumerable<string>)controller.Menu().Model).ToArray();

            Assert.AreEqual(4, categories.Length);

            Assert.AreEqual("c2", categories[0]);
            Assert.AreEqual("c3", categories[1]);
            Assert.AreEqual("c4", categories[2]);
            Assert.AreEqual("c5", categories[3]);


        }


        [Test]
        public void Indicates_Selected_Category()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(m => m.Products).Returns(new Product[] { 
               new Product{ ProductID = 1, Name = "P1", Category = "c2"},
               new Product{ ProductID = 2, Name = "P2", Category = "c2"},
               new Product{ ProductID = 3, Name = "P3", Category = "c3"},
               new Product{ ProductID = 4, Name = "P4", Category = "c4"},
               new Product{ ProductID = 5, Name = "P5", Category = "c5"}}.AsQueryable());

            NavController controller = new NavController(mock.Object);

            string categorySelected = "c2";

            string result = controller.Menu(categorySelected).ViewBag.SelectedCategory;

            Assert.AreEqual(categorySelected, result);


        }

        
    }
}
