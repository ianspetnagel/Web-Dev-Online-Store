using Microsoft.AspNetCore.Mvc.Rendering;
using ProductsAppRPSpetnagel.Models;


namespace ProductsAppRPSpetnagel.Models.ModelsVM
{
    public class AddEditProductVM
    {
        public SelectList CatList { get; set; } // for drop down categories
        public Product Prod { get; set; }
        public string ButtonText { get; set; } // whether it is Add Product or Edit Product
    }
}
