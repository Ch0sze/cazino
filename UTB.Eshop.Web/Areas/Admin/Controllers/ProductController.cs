using Microsoft.AspNetCore.Mvc;
using UTB.Eshop.Application.Abstraction;
using UTB.Eshop.Domain.Entities;
using UTB.Eshop.Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using UTB.Eshop.Infrastructure.Identity.Enums;

namespace UTB.Eshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
    public class ProductController : Controller
    {
        IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            IList<Product> products = _productService.Select();
            return View(products);
        }

        [HttpGet] //vychozi atribut pro akcni metody
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _productService.Create(product);

            return RedirectToAction(nameof(ProductController.Index));
        }

        public IActionResult Delete(int id)
        {
            bool deleted = _productService.Delete(id);

            if (deleted)
                return RedirectToAction(nameof(ProductController.Index));
            else
                return NotFound();
        }
    }
}
