using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PoeAgriEnergyConnect.Models;


namespace PoeAgriEnergyConnect.Pages.Farm
{
    public class AddProductModel : PageModel
    {
        private readonly DatabaseHelper _dbContext = new DatabaseHelper();
        [BindProperty]
        public Product Product { get; set; }
       
        public void OnGet()
        {
           
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                string FarmerId = System.Configuration.ConfigurationManager.AppSettings["FarmerId"];
              

                //if (!ModelState.IsValid)
                //{
                //    return Page();
                //}
                
                var newProduct = new Product
                {
                    Name = Product.Name,
                    Category = Product.Category,
                    ProductionDate = Product.ProductionDate,
                    FarmerId = FarmerId
                };

                 _dbContext.InsertProduct(newProduct);

                return RedirectToPage("/Farmers/ProductList");
            }
            catch (Exception)
            {

            }

            return RedirectToPage("/Farmers/ProductList");
        }
    }
}
