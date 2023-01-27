using Forms.Web.Data;
using Forms.Web.Models;
using Forms.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Forms.Web.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

       
        public IActionResult Index()
        {
            var categories = _db.Categories.Where(x => !x.IsDelete).ToList();
            
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create() // اول عملية بتستدعي فورم او فيو الاضافة
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateCategoryViewModel input)
        {
            //code to save in database
            if (ModelState.IsValid) //Ensure the condition is true
            {
                var category = new Category();
                category.Name = input.Name;
                category.UpdatedAt = DateTime.Now;
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["msg"] = "Item added successfuly";
                return RedirectToAction("Index");
            }
            return View(input); //عشان مضيعش البيانات اللي تعبت من قبل
          
        }
        [HttpGet]
        public IActionResult Update(int Id)
        {
            var category = _db.Categories.SingleOrDefault(x => x.Id == Id && !x.IsDelete);
            if (category == null)
            {
                return NotFound();
            }
            var vm = new UpdateCategoryViewModel();
            vm.Id = category.Id;
            vm.Name = category.Name;
            return View(vm);
        }

        [HttpPost]
        public IActionResult Update(UpdateCategoryViewModel input)
        {
            if (ModelState.IsValid)
            {
                var category = _db.Categories.SingleOrDefault(x => x.Id == input.Id && !x.IsDelete);
            if (category == null)
            {
                return NotFound();
            }
            category.Name = input.Name;
            _db.Categories.Update(category);
            _db.SaveChanges();
            TempData["msg"] = "s:Item Updated Successfully ";
            return RedirectToAction("Index");

            }
            return View(input);

        }

        public IActionResult Delete(int Id)
        {
            var category = _db.Categories.SingleOrDefault(x=> x.Id == Id && !x.IsDelete);
            if(category == null)
            {
                return NotFound();
            }
            category.IsDelete = true;

            _db.Categories.Update(category);
            _db.SaveChanges();

            TempData["msg"] = "s: Item deleted succesfully";
            return RedirectToAction("Index");
        }
    }
}
