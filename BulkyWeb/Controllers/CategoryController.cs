using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
	public class CategoryController : Controller
	{
		private readonly ICategoryRepository _categoryRepo;
		public CategoryController(ICategoryRepository db)
		{
			_categoryRepo = db;
		}
		public IActionResult Index()
		{
			List<Category> categories = _categoryRepo.GetAll().ToList();
			return View(categories);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Category category)
		{
			if (category.Name == category.DisplayOrder.ToString())
			{
				ModelState.AddModelError("Name", "Name and Display Order cannot be the same");
			}
			if (ModelState.IsValid)
			{
				_categoryRepo.Add(category);
				_categoryRepo.Save();
				TempData["success"] = "Category created Successfully";
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}

			Category categoryFromDb = _categoryRepo.Get(u => u.Id == id);
			if (categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb);

		}

		[HttpPost]
		public IActionResult Edit(Category obj)
		{
			if (ModelState.IsValid)
			{
				_categoryRepo.Update(obj);
				_categoryRepo.Save();
				TempData["success"] = "Category updated Successfully";
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Category category = _categoryRepo.Get(u => u.Id == id);
			if (category == null)
			{
				return NotFound();
			}
			return View(category);
		}


		[HttpPost]
		public IActionResult Delete(Category obj)
		{
			if (obj != null)
			{
				_categoryRepo.Remove(obj);
				_categoryRepo.Save();
				TempData["success"] = "Category deleted Successfully";
				return RedirectToAction("Index");
			}
			return View();
		}


	}
}