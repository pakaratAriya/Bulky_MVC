using BulkyRazor_Temp.Data;
using BulkyRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyRazor_Temp.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private ApplicationDbContext _db;
        public Category category { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int? id)
        {
            if (id != null && id >= 0)
            {
                category = _db.Categories.Find(id);
            }
        }

        public IActionResult OnPost(Category cat)
        {
            _db.Remove(cat);
            _db.SaveChanges();
            TempData["Success"] = $"Deleted {cat.Name} category successfully";
            return RedirectToPage("Index");
        }

    }
}
