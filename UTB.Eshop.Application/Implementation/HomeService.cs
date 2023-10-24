using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTB.Eshop.Application.Abstraction;
using UTB.Eshop.Application.ViewModels;
using UTB.Eshop.Infrastructure.Database;

namespace UTB.Eshop.Application.Implementation
{
    public class HomeService : IHomeService
    {
        public CarouselProductViewModel GetHomeIndexViewModel()
        {
            CarouselProductViewModel viewModel = new CarouselProductViewModel();
            viewModel.Products = DatabaseFake.Products;
            viewModel.Carousels = DatabaseFake.Carousels;
            return viewModel;
        }
    }
}
