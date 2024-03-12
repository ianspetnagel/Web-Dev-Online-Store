using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductsAppRPSpetnagel.Models;
using ProductsAppRPSpetnagel.Repositories;

namespace ProductsAppRPSpetnagel.Pages
{
    public class ShowProductsModel : PageModel
    {
        private readonly IProductsRepository _rep;

        public ShowProductsModel(IProductsRepository rep)
        {
            // Injection of IProductsRepository
            _rep = rep;
        }

        public SelectList CatList { get; set; }
        public List<Product> PList { get; set; }

        [BindProperty]
        public int CategoryIdSelected { get; set; }

        public void OnGet()
        {
            List<Category> CList = _rep.GetCategories();

            // Convert List<Category> to SelectList so that it can
            // be bound to a drop-down
            CatList = new SelectList(CList, "CategoryId", "CategoryName");
            CategoryIdSelected = CList[0].CategoryId;
            PList = _rep.GetProductsByCategory(CategoryIdSelected);
        }

        public void OnPost()
        {
            List<Category> CList = _rep.GetCategories();
            CatList = new SelectList(CList, "CategoryId", "CategoryName");
            PList = _rep.GetProductsByCategory(CategoryIdSelected);
        }

        public void OnPostApplyDiscount(double amtid, int prodid)
        {
            List<Category> CList = _rep.GetCategories();
            CatList = new SelectList(CList, "CategoryId", "CategoryName");
            _rep.ApplyDiscount(prodid, amtid);
            PList = _rep.GetProductsByCategory(CategoryIdSelected);
        }

        public void OnPostIncreasePrice(double amtid, int prodid)
        {
            List<Category> CList = _rep.GetCategories();
            CatList = new SelectList(CList, "CategoryId", "CategoryName");
            _rep.IncreasePrice(prodid, amtid);
            PList = _rep.GetProductsByCategory(CategoryIdSelected);
        }

        public void OnPostDeleteProd(int prodid)
        {
            List<Category> CList = _rep.GetCategories();
            CatList = new SelectList(CList, "CategoryId", "CategoryName");
            _rep.DeleteProduct(prodid);
            PList = _rep.GetProductsByCategory(CategoryIdSelected);
        }
    }
}
