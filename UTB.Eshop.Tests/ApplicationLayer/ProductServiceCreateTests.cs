using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using Moq;

using UTB.Eshop.Application.Abstraction;
using UTB.Eshop.Infrastructure.Database;
using UTB.Eshop.Domain.Entities;
using UTB.Eshop.Application.Implementation;

namespace UTB.Eshop.Tests.ApplicationLayer
{
    public class ProductServiceCreateTests
    {
        [Fact]
        public void Create_Success()
        {
            // Arrange

            //Nainstalovan Nuget package: Microsoft.EntityFrameworkCore.InMemory
            //databazi vytvori v pameti
            DbContextOptions options = new DbContextOptionsBuilder<EshopDbContext>()
                                       .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                                       .Options;
            var databaseContext = new EshopDbContext(options);
            databaseContext.Database.EnsureCreated();
            //smazani inicializacnich dat, pokud existuji
            databaseContext.Products.RemoveRange(databaseContext.Products);
            databaseContext.SaveChanges();



            ProductService service = new ProductService(databaseContext);


            Product testProduct = GetTestProduct();



            //Act
            service.Create(testProduct);



            // Assert
            Assert.Single(databaseContext.Products);

            Product addedProduct = databaseContext.Products.First();
            Assert.Equal(testProduct.Id, addedProduct.Id);
            Assert.NotNull(addedProduct.Name);
            Assert.Matches(testProduct.Name, addedProduct.Name);
            Assert.Equal(testProduct.Price, addedProduct.Price);
            Assert.NotNull(addedProduct.ImageSrc);

        }



        Product GetTestProduct()
        {
            return new Product()
            {
                Id = 1,
                Name = "produkt",
                Price = 10,
                ImageSrc = String.Empty,
            };
        }

    }
}
