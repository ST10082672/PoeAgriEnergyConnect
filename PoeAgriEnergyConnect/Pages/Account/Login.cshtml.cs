using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PoeAgriEnergyConnect.Models;
using System.Data.SqlClient;


namespace PoeAgriEnergyConnect.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly DatabaseHelper _dbContext = new DatabaseHelper();    
        [BindProperty]
        public LoginViewModel Input { get; set; }
        public string ErrorMessage { get; set; }
        public void OnGet()
        {
            
        }
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<Farmer> farmers = _dbContext.ReadFarmersData();// await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: false);

                    Farmer result = farmers.Find(u => u.Email.Equals(Input.Email) && u.Password.Equals(Input.Password));

                    if (result != null)
                    {

                        System.Configuration.ConfigurationManager.AppSettings["FarmerId"] = result.Id + "";
                        if (result.UserType.Equals("Farmer"))
                        {

                            return RedirectToPage("/Farmers/AddProduct");
                        }
                        else if (result.UserType.Equals("Employee"))
                        {
                            return RedirectToPage("/Employees/AddFarmer");
                        }

                    }
                    else
                    {
                        ErrorMessage = "Invalid login attempt.";
                        ModelState.AddModelError(string.Empty, ErrorMessage);

                    }
                }

                // If we got this far, something failed; redisplay the form
            }
            catch (Exception)
            {

            }
            return Page();
        }

      
    }
}
