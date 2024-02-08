using BulkyRazor_Temp.Data;
using BulkyRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        ApplicationDbContext _db;
        public Category category { get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            _db.Add(category);
            _db.SaveChanges();
            TempData["Success"] = $"Created {category.Name} category successfully";
            return RedirectToPage("Index");
        }
    }
}
