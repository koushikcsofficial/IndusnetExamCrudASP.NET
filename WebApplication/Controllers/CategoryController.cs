using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class CategoryController : Controller
    {

        private CategoryLogics categoryLogics = new CategoryLogics();

        [HttpGet]
        public ActionResult Index()
        {
            var data = categoryLogics.GetCategories();
            return View(data);
        }

        [HttpPost]
        public ActionResult Create(string Category_Name)
        {
            var result = categoryLogics.CreateCategory(Category_Name);
            if (result == null)
            {
                ViewBag.Msg = "Category Already Exists or Error while inserting the category";
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            var data = categoryLogics.GetCategory(Id);
            if (data == null)
                return RedirectToAction("Index");
            ViewData["Id"] = data.Id;
            ViewData["Value"] = data.Category_Name;
            return View();
        }
        [HttpPost]
        public  ActionResult Edit(int? Id, string category_Name)
        {
            var data = categoryLogics.EditCategory(Id, category_Name);
            if (data == null)
            {
                ViewBag.Msg = "Category already exists";
                return RedirectToAction("Index", "Category");
            }
            return RedirectToAction("Index", "Category");
        }
    }
}