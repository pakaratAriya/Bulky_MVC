using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public ProductController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public IActionResult Index()
		{
			List<Product> products = _unitOfWork.Product.GetAll().ToList();
			return View(products);
		}

		public IActionResult Create()
		{
			IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
			{
				Text = u.Name,
				Value = u.Id.ToString(),
			});
			ViewBag.CategoryList = CategoryList;
			return View();
		}

		[HttpPost]
		public IActionResult Create(Product product)
		{
			if (ModelState.IsValid)
			{
				_unitOfWork.Product.Add(product);
				_unitOfWork.Save();
				TempData["success"] = "Product created successfully";
				return RedirectToAction("index");
			}
			return View();
		}

		public IActionResult Edit(int? id)
		{
			if(id == null || id == 0)
			{
				return NotFound();
			}
			Product product = _unitOfWork.Product.Get(u => u.Id == id);
			if(product == null)
			{
				return NotFound();
			}
			return View(product);
		}

		[HttpPost]
		public IActionResult Edit(Product product)
		{
			if(product == null)
			{
				return NotFound();
			}
			if(ModelState.IsValid)
			{
				_unitOfWork.Product.Update(product);
				_unitOfWork.Save();
				TempData["success"] = "Product updated successfully";
				return RedirectToAction("index");
			}
			return View();
		}

		public IActionResult Delete(int? id)
		{
			if(id == null || id==0)
			{
				return NotFound();
			}
			Product product = _unitOfWork.Product.Get(u => u.Id == id);
			if( product == null)
			{
				return NotFound();
			}
			return View(product);
		}

		[HttpPost]
		public IActionResult Delete(Product product)
		{
			if( product == null)
			{
				return View();
			}
			_unitOfWork.Product.Remove(product);
			_unitOfWork.Save();
			TempData["success"] = "Product deleted successfully";
			return RedirectToAction("index");
		}
	}
}
