using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PoeAgriEnergyConnect.Models;



namespace PoeAgriEnergyConnect.Pages.Employees
{
    public class AddFarmerModel : PageModel
    {
        private readonly DatabaseHelper _dbContext = new DatabaseHelper();

        [BindProperty]
        public Farmer Farmer { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                var newFarmer = new  Farmer
                {
                    Name = Farmer.Name,
                    Address = Farmer.Address,
                    Email = Farmer.Email,
                    Password = Farmer.Password ,
                    PhoneNumber = Farmer.PhoneNumber,
                    UserType = Farmer.UserType
                };

                 _dbContext.InsertFarmer(newFarmer);

                return RedirectToPage("/Employees/FarmerList");
            }
            catch (Exception)
            {

               
            }
            return RedirectToPage("/Employees/FarmerList");
        }
    }
}
