﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Database;
using SportsStore.Domain.Concrete;

namespace SportsStore.WebUI.Infrastructure
{
    public class NInjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NInjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            //Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product> { 
            //    new Product{ Name = "FootBall", Price = 25},
            //    new Product{ Name = "Surf board", Price = 179},
            //    new Product{ Name = "Running shoes", Price = 95}}.AsQueryable());

            //ninjectKernel.Bind<IProductRepository>().ToConstant(mock.Object);


            ninjectKernel.Bind<IProductRepository>().To<EFProductRepository>();
            ninjectKernel.Bind<IAutProvider>().To<FormsAuthProvider>();
        }

    }
}