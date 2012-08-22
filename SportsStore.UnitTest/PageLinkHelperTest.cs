using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.Web.Mvc;
using SportsStore.WebUI.Models;
using SportsStore.WebUI.HtmlHelpers;

namespace SportsStore.UnitTest
{
    [TestFixture]
    public  class PageLinkHelperTest
    {
        [Test]
        public void Can_Generate_Page_Links()
        {
            HtmlHelper myHelper = null;

            PageInfo pagingInfo = new PageInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10
            };

            Func<int, string> PageUrlDelegate = i => "Page" + i;

            MvcHtmlString result = myHelper.PageLink(pagingInfo,PageUrlDelegate);

            Assert.AreEqual(result.ToString(), @"<a href=""Page1"">1</a><a class=""selected"" href=""Page2"">2</a><a href=""Page3"">3</a>");

        }
    }
}
