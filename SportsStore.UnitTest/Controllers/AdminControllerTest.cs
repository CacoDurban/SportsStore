using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using System.Web.Mvc;

namespace SportsStore.UnitTest.Controllers
{
    [TestFixture]
    public class AdminControllerTest
    {
        
        [Test]
        public void Index_Contains_All_Products()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(m => m.Products).Returns(new Product[] { 
               new Product{ ProductID = 1, Name = "P1"},
               new Product{ ProductID = 2, Name = "P2"},
               new Product{ ProductID = 3, Name = "P3"},
               new Product{ ProductID = 4, Name = "P4"},
               new Product{ ProductID = 5, Name = "P5"}}.AsQueryable());

            AdminController controller = new AdminController(mock.Object);

            Product[] products = ((IEnumerable<Product>)controller.Index().ViewData.Model).ToArray();

            Assert.AreEqual(5, products.Length);
        }

        [Test]
        public void Can_Edit_Product()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(m => m.Products).Returns(new Product[] { 
               new Product{ ProductID = 1, Name = "P1"},
               new Product{ ProductID = 2, Name = "P2"},
               new Product{ ProductID = 3, Name = "P3"},
               new Product{ ProductID = 4, Name = "P4"},
               new Product{ ProductID = 5, Name = "P5"}}.AsQueryable());

            AdminController controller = new AdminController(mock.Object);

            var product = controller.Edit(1).ViewData.Model as Product;

            Assert.AreEqual(1, product.ProductID);
        }

        [Test]
        public void Cannot_Edit_NonExistent_Product()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(m => m.Products).Returns(new Product[] { 
               new Product{ ProductID = 1, Name = "P1"},
               new Product{ ProductID = 2, Name = "P2"},
               new Product{ ProductID = 3, Name = "P3"},
               new Product{ ProductID = 4, Name = "P4"},
               new Product{ ProductID = 5, Name = "P5"}}.AsQueryable());

            AdminController controller = new AdminController(mock.Object);

            var product = controller.Edit(12).ViewData.Model as Product;

            Assert.IsNull(product);
        }

        [Test]
        public void Can_Save_Valid_Changes()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            AdminController controller = new AdminController(mock.Object);

            Product product = new Product() { Name = "Test" };

            ActionResult result = controller.Edit(product,null);

            mock.Verify(m => m.SaveProduct(product));

            Assert.IsNotInstanceOf(typeof(ViewResult), result);
        }

        [Test]
        public void Cannot_Save_InValid_Changes()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            AdminController controller = new AdminController(mock.Object);

            Product product = new Product() { Name = "Test" };

            controller.ModelState.AddModelError("error", "error");

            ActionResult result = controller.Edit(product,  null);

            mock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());

            Assert.IsInstanceOf(typeof(ViewResult), result);

        }

        [Test]
        public void Can_Delete_Valid_Products()
        {
            Product product = new Product() { Name = "Test", ProductID = 1 };

            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(x => x.Products).Returns(new Product[]
            {
                new Product{ ProductID = 1, Name = "Test"}
            }.AsQueryable());

            AdminController controller = new AdminController(mock.Object);

            controller.Delete(product.ProductID);

            mock.Verify(x => x.DeleteProduct(product));

        }


        [Test]
        public void Cannot_Delete_InValid_Products()
        {
            Product product = new Product() { Name = "Test", ProductID = 1 };

            Mock<IProductRepository> mock = new Mock<IProductRepository>();

            mock.Setup(x => x.Products).Returns(new Product[]
            {
                new Product{ ProductID = 1, Name = "Test"}
            }.AsQueryable());

            AdminController controller = new AdminController(mock.Object);

            controller.Delete(33);

            mock.Verify(x => x.DeleteProduct(It.IsAny<Product>()),Times.Never());

        }

    }
}
