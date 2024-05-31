using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PoeAgriEnergyConnect.Models;


namespace PoeAgriEnergyConnect.Pages.Farmers
{
    public class ProductListModel : PageModel
    {
        private readonly DatabaseHelper _dbContext = new DatabaseHelper();

        public List<Product> Products { get; set; }
        public void OnGet()
        {
            try
            {
                string FarmerId = System.Configuration.ConfigurationManager.AppSettings["FarmerId"];
                Products = _dbContext.ReadProductsData().FindAll(x => x.FarmerId == FarmerId);
            }
            catch (Exception)
            {

            }
        }
    }
}
