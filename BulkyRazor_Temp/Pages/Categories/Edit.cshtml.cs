using BulkyRazor_Temp.Data;
using BulkyRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyRazor_Temp.Pages.Categories
{
	[BindProperties]
	public class EditModel : PageModel
	{
		private ApplicationDbContext _db;
		public Category category { get; set; }
		public EditModel(ApplicationDbContext db)
		{
			_db = db;
		}
		public void OnGet(int? id)
		{
			if (id != null && id > 0)
			{
				category = _db.Categories.Find(id);
			}
		}

		public IActionResult OnPost()
		{
			_db.Update(category);
			_db.SaveChanges();
			TempData["Success"] = $"Edit {category.Name} category successfully";
			return RedirectToPage("Index");
		}
	}
}
