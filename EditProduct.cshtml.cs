using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductsAppRPSpetnagel.Models;
using ProductsAppRPSpetnagel.Models.ModelsVM;
using ProductsAppRPSpetnagel.Repositories;

namespace ProductsAppRPSpetnagel.Pages
{
    public class EditProductModel : PageModel
    {
        IProductsRepository _rep;
        public EditProductModel(IProductsRepository rep)
        {
            _rep = rep;
        }
        [BindProperty]
        public AddEditProductVM ProdVM { get; set; } // product from UI
        public string Status = "";
        public void OnGet(int prodid)
        {
            ProdVM = new AddEditProductVM();
            Product prod = _rep.GetProductById(prodid);
            ProdVM.Prod = prod;
            ProdVM.ButtonText = "Update Product Via Partial";
            List<Category> CList = _rep.GetCategories();
            // convert List<category> to List<SelectListItem> so that it can
            // be bound to a drop down
            ProdVM.CatList = new SelectList(CList, "CategoryId", "CategoryName");
        }
        public void OnPost()
        {
            ModelState.Remove("CatList"); // without these, it creates error
            ModelState.Remove("ButtonText");
            ModelState.Remove("Prod.Category"); // when database is involved

            if (!ModelState.IsValid)
            {
                return;
            }
            List<Category> CList = _rep.GetCategories();
            ProdVM.CatList = new SelectList(CList, "CategoryId", "CategoryName");
            ProdVM.ButtonText = "Update Product Via Partial";
            bool ret = false;
            try
            {
                ret = _rep.UpdateProduct(ProdVM.Prod);
            }
            catch (Exception ex)
            {
                Status = ex.Message;
            }
            if (ret)
                Status = "Product updated successfully..";
            else
                Status = "Problem in updating Product..";
        }
    }
}
