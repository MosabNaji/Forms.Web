using Forms.Web.Data;
using Forms.Web.Models;
using Forms.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Forms.Web.Services;
using System.Threading.Tasks;

namespace Forms.Web.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _db;
        private IFileService _fileService;
        private IEmailService _emailService;

        public ProductController(ApplicationDbContext db , IFileService fileService, IEmailService emailService)
        {
            _db = db;
            _fileService = fileService;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            var products = _db.Products.Include(x => x.Category).Where(x => !x.IsDelete).OrderByDescending(x => x.CreatedAt).ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //بتاخد 3 معاملات 1 الليست 2 الاي دي 3 اي خاصية تانية مثلا الاسم
            ViewData["categoryList"] = new SelectList(_db.Categories.Where(x => !x.IsDelete).ToList(),"Id","Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(/*[FromForm]*/CreateProductViewModel input)
        {

            if (ModelState.IsValid)
            { //check
                var nameIsExist = _db.Products.Any(x => x.Name == input.Name);
                if (nameIsExist)
                {
                    TempData["msg"] = "e:Product was already exist !";
                    ViewData["categoryList"] = new SelectList(_db.Categories.Where(x => !x.IsDelete).ToList(), "Id", "Name");
                    return View(input);
                }

                var product = new Product();
                product.Name = input.Name;
                product.Description = input.Description;
                product.Price = input.Price;
                product.CategoryId = input.CategoryId;
                if(input.ImageURL != null)
                {
                    product.ImageURL = await _fileService.SaveFile(input.ImageURL , "Images");
                }
                 
                product.CreatedAt = DateTime.Now;
                _db.Products.Add(product);
                _db.SaveChanges();

                //Send Email to Admins
                var Admins = _db.Admins.Where(x => !x.IsDelete).ToList();
                foreach(var admin in Admins)
                {
                   await _emailService.Send(admin.Email ,"New Product" ,$"New Product with name : {product.Name}"  );
                }

                TempData["msg"] = "s:Product was added successfully";
                return RedirectToAction("Index");
            }
            ViewData["categoryList"] = new SelectList(_db.Categories.Where(x => !x.IsDelete).ToList(), "Id", "Name");
            return View(input);
        }
    }
}
