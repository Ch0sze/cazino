using System;
using System.Diagnostics;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Moq;
using UTB.Eshop.Application.Abstraction;
using UTB.Eshop.Domain.Entities;
using UTB.Eshop.Infrastructure.Database;
using UTB.Eshop.Web.Areas.Admin.Controllers;

namespace UTB.Eshop.Tests.Admin.ProductController
{
    public class ProductControllerCreateTests
    {
        [Fact]
        public void Create_success()
        {
            //Arrange
            DatabaseFake.Products.Clear();
            Mock<IProductService> productServiceMock = new Mock<IProductService>();
            productServiceMock.Setup(productService => productService.Create(It.IsAny<Product>()))
                                        .Callback<Product>(product => DatabaseFake.Products.Add(product) );

            var product = GetProduct();

            var productController = new Web.Areas.Admin.Controllers.ProductController(productServiceMock.Object);


            //Act
            var actionResult = productController.Create(product);


            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(actionResult);
            Assert.NotNull(redirectToActionResult.ActionName);
            Assert.Equal(nameof(Web.Areas.Admin.Controllers.ProductController.Index), redirectToActionResult.ActionName);

            Assert.NotEmpty(DatabaseFake.Products);
            Assert.Single(DatabaseFake.Products);
        }

        Product GetProduct()
        {
            return new Product()
            {
                Id = 1,
                Name = "Produkt",
                Price = 1.0,
                Description = String.Empty,
                ImageSrc = "/superimage.jpeg"
            };
        }
    }
}

