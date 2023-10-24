using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTB.Eshop.Domain.Entities;

namespace UTB.Eshop.Infrastructure.Database
{
    internal class DatabaseInit
    {
        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            products.Add(new Product
            {
                Id = 1,
                Name = "Rohlík",
                Description = "nejlepší rohlík na světě",
                Price = 4,
                ImageSrc = "/img/products/produkty-01.jpg"
            });
            products.Add(new Product
            {
                Id = 2,
                Name = "Chleba",
                Description = "nej chleba ve sluneční soustavě",
                Price = 50,
                ImageSrc = "/img/products/produkty-02.jpg"
            });
            products.Add(new Product
            {
                Id = 3,
                Name = "Vánočka",
                Description = "nic moc, ale taky ji máme",
                Price = 40,
                ImageSrc = "/img/products/produkty-03.jpg"
            });
            products.Add(new Product
            {
                Id = 4,
                Name = "bageta",
                Description = "nejlepší bageta ve vesmíru",
                Price = 25,
                ImageSrc = "/img/products/produkty-05.jpg"
            });

            return products;
        }

        public List<Carousel> GetCarousel()
        {
            List<Carousel> carousels = new List<Carousel>();

            carousels.Add(new Carousel()
            {
                Id = 1,
                ImageSrc = "/img/carousel/how-to-become-an-information-technology-specialist160684886950141.jpg",
                ImageAlt = "First"
            });
            carousels.Add(new Carousel()
            {
                Id = 2,
                ImageSrc = "/img/carousel/Information-Technology-1-1.jpg",
                ImageAlt = "Second"
            });
            carousels.Add(new Carousel()
            {
                Id = 3,
                ImageSrc = "/img/carousel/itec-index-banner.jpg",
                ImageAlt = "Third"
            });

            return carousels;
        }
    }
}
