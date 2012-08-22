using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;

namespace SportsStore.UnitTest
{
    [TestFixture]
    public class CartControllerTest
    {
        [Test]
        public void Cant_CheckOut_if_a_Cart_is_Empty()
        {
            Cart cart = new Cart();

            CartController controller = new CartController(null);

            var IsValid = controller.CheckOut(cart, null).ViewData.ModelState.IsValid;

            Assert.IsFalse(IsValid);
        }

    }
}
