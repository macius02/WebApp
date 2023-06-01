using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Projekt.Authentication;
using System.Xml.Linq;

namespace Projekt.Pages.Users
{
    [RequireAuth(RequiredRole = "client")]
    [BindProperties]
    public class AboutMeModel : PageModel
    {

        public User user { get; set; } = default!;
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Surname { get; set; } = "";
        [Required]
        public string Email { get; set; } = "";
        [Required]
        public string PhoneNumber { get; set; } = "";
        [Required]
        public string ObservedCategory { get; set; } = "";
        public string OldPassword { get; set; } = "";
        public string? Password { get; set; } = "";
        public string? ConfirmPassword { get; set; } = "";
        public string errorMessage = "";
        public string successMessage = "";
        public int Id;
        public void OnGet()
        {
            Name = HttpContext.Session.GetString("Name") ?? "";
            Surname = HttpContext.Session.GetString("Surname") ?? "";
            Email = HttpContext.Session.GetString("Email") ?? "";
            PhoneNumber = HttpContext.Session.GetString("PhoneNumber") ?? "";
            ObservedCategory = HttpContext.Session.GetString("ObservedCategory") ?? "";
            OldPassword = HttpContext.Session.GetString("OldPassword") ?? "";
        }
        private readonly Projekt.Userdb _context;

        public AboutMeModel(Projekt.Userdb context) {
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync(int? Id)
        {
            string submitButton = Request.Form["action"];
            Id = (int)HttpContext.Session.GetInt32("Id");

            var item = _context.Users.FirstOrDefault(m => m.Id == Id);


            if (submitButton.Equals("profile"))
            {
                item.Name = Name;
                item.Surname = Surname;
                item.Email = Email;
                item.PhoneNumber = PhoneNumber;
                item.ObservedCategory = ObservedCategory;
                successMessage = "Profile updated succesfully";
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("Name", item.Name);
                HttpContext.Session.SetString("Surname", item.Surname);
                HttpContext.Session.SetString("PhoneNumber", item.PhoneNumber);
                HttpContext.Session.SetString("Email", item.Email);
                HttpContext.Session.SetString("ObservedCategory", item.ObservedCategory);

            }
            else if (submitButton.Equals("password"))
            {
                if (Password == null || ConfirmPassword == null)
                {
                    errorMessage = "There is no new password to set";
                    return Page();
                } 
                else if(!Password.Equals(ConfirmPassword))
                {
                    errorMessage = "Password and Confirm Password are not equal";
                    return Page();
                }
                else
                {
                    item.Password = Password;
                    successMessage = "Password updated successfully";
                    await _context.SaveChangesAsync();
                    HttpContext.Session.SetString("Password", item.Password);
                }
            }
            
            return Page();
        }

    }
}
