using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PoeAgriEnergyConnect.Models;

namespace PoeAgriEnergyConnect.Pages.Employees
{
    public class FarmerListModel : PageModel
    {
        private readonly DatabaseHelper _dbContext = new DatabaseHelper();
        public List<Product> Products { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? FarmerId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ProductType { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }

        public SelectList FarmerList { get; set; }
        public Farmer SelectedFarmer { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                // Load all farmers for the dropdown
                var farmers =  _dbContext.ReadFarmersData().ToList();
                FarmerList = new SelectList(farmers, "Id", "Name", FarmerId);

                if (!string.IsNullOrEmpty(FarmerId))
                {
                    // Load the selected farmer
                    SelectedFarmer = _dbContext.ReadFarmersData().Find(x => x.Id.ToString() == FarmerId);

                    if (SelectedFarmer != null)
                    {
                        // Load products for the selected farmer
                        var productsQuery = _dbContext.ReadProductsData()
                            .Where(p => p.FarmerId == FarmerId)
                            //.Include(p => p.Farmer)
                            .AsQueryable();

                        if (!string.IsNullOrEmpty(ProductType))
                        {
                            productsQuery = productsQuery.Where(p => p.Category.Contains(ProductType));
                        }

                        if (StartDate.HasValue)
                        {
                            productsQuery = productsQuery.Where(p => p.ProductionDate >= StartDate.Value);
                        }

                        if (EndDate.HasValue)
                        {
                            productsQuery = productsQuery.Where(p => p.ProductionDate <= EndDate.Value);
                        }

                        Products =  productsQuery.ToList();
                    }
                }
            }
            catch (Exception)
            {

               
            }
        }
    }
}
