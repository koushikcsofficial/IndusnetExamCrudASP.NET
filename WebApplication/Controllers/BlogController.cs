using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class BlogController : Controller
    {
        private BlogLogics blogLogics;
        private CategoryLogics categoryLogics;

        [HttpGet]
        public ActionResult Index()
        {
            blogLogics = new BlogLogics();
            categoryLogics = new CategoryLogics();

            var data = categoryLogics.GetCategories();
            if(data==null || data.Count == 0)
            {
                return RedirectToAction("Index", "Category");
            }
            var obj= blogLogics.GetBlogs();
            return View(obj);
        }

        [HttpGet]
        public ActionResult Create()
        {
            categoryLogics = new CategoryLogics();
            var data = categoryLogics.GetCategories();
            return View(data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Blog_Headline, string Blog_Description, int? Category_Id)
        {
            if(!String.IsNullOrWhiteSpace(Blog_Headline)&& !String.IsNullOrWhiteSpace(Blog_Description)&& (Category_Id != null))
            {
                blogLogics = new BlogLogics();
                var data = blogLogics.CreateBlog(Blog_Headline, Blog_Description,GetIp(), Category_Id);

                if (data == null)
                {
                    ViewBag.Msg = "Internal error occured while creating the blog";
                }

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Msg = "Field can't be left empty";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(int? Id)
        {
            blogLogics = new BlogLogics();
            categoryLogics = new CategoryLogics();
            if (Id == null)
                return View("Error");

            var data = blogLogics.GetBlog(Id);
            var CategoryList = categoryLogics.GetCategories();

            if (data == null)
            {
                ViewBag.Msg = "No Blog found with this Blog-Id";
                return View("Error");
            }

            ViewBag.Blog_Id = data.Id;
            ViewBag.Blog_Headline = data.Blog_Headline;
            ViewBag.Blog_Description = data.Blog_Description;

            return View(CategoryList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? Blog_Id, string Blog_Headline, string Blog_Description, int? Category_Id)
        {
            blogLogics = new BlogLogics();
            if (!String.IsNullOrWhiteSpace(Blog_Headline) && !String.IsNullOrWhiteSpace(Blog_Description) && (Category_Id != null) && (Blog_Id!=null))
            {
                var data = blogLogics.EditBlog(Blog_Id, Blog_Headline, Blog_Description, Category_Id);

                if (data == null)
                {
                    ViewBag.Msg = "Internal error occured while updating the blog";
                }
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Msg = "Fields can't be left empty";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int? Id)
        {
            blogLogics = new BlogLogics();
            if (Id == null)
                return View("Error");

            BlogLogics blg = new BlogLogics();
            var data = blg.DeleteBlog(Id);

            if (data)
            {
                ViewBag.Msg = "Blog deleted successfully";

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Msg = "Error occured while deleting";
                return View("Error");
            }
        }


        [NonAction]
        private string GetIp()
        {
            string ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(ip))
            {
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            return ip;
        }
    }
}