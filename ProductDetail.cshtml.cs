using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductsAppRPSpetnagel.Models;
using ProductsAppRPSpetnagel.Models.ModelsVM;
using ProductsAppRPSpetnagel.Repositories;

namespace ProductsAppRPSpetnagel.Pages
{
    public class ProductDetailModel : PageModel
    {
        IProductsRepository _rep;
        public ProductDetailModel(IProductsRepository rep)
        {
            _rep = rep;
        }
        public Product Prod { get; set; }
        public string Status = "";
        public void OnGet(int prodid)
        {
            Prod = _rep.GetProductById(prodid);
        }
    }
}
