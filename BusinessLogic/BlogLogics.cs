using DataAccess;
using DomainModels;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.Entity;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public partial class BlogLogics:General
    {
        private Blog blog;
        public Blog CreateBlog(string Blog_Headline,string Blog_Description, string Blog_CreatedFrom, int? category_Id)
        {
            blog = new Blog();
            blog.Blog_Headline = Blog_Headline.ToString().Trim();
            blog.Blog_Description = Blog_Description.ToString().Trim();
            blog.Blog_CreatedFrom = Blog_CreatedFrom.ToString().Trim();
            blog.Blog_CreatedAt = CurrentIndianTime();
            using (DatabaseContext db = new DatabaseContext())
            {
                try
                {
                    blog.Category = db.Categories.Find(category_Id);
                    db.Blogs.Add(blog);
                    db.SaveChanges();
                    return blog;
                }
                catch(Exception msg)
                {
                    EventLog log = new EventLog();
                    log.WriteEntry(msg.ToString()+" "+CurrentIndianTime());
                    return null;
                }
            }
        }

        public List<Blog> GetBlogs()
        {
            using(DatabaseContext db = new DatabaseContext())
            {
                return db.Blogs.Include(blg=>blg.Category).ToList();
            }
        }
        public Blog GetBlog(int? Id)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                return db.Blogs.Include(blg => blg.Category).Where(blg=>blg.Id==Id).FirstOrDefault();
            }
        }

        public Blog EditBlog(int? Blog_Id, string Blog_Headline, string Blog_Description, int? category_Id)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                blog = db.Blogs.Find(Blog_Id);
                if (blog == null)
                    return null;
                blog.Blog_Headline = Blog_Headline.ToString().Trim();
                blog.Blog_Description = Blog_Description.ToString().Trim();
                try
                {
                    blog.Category = db.Categories.Find(category_Id);
                    db.SaveChanges();
                    return blog;
                }
                catch(Exception msg)
                {
                    EventLog log = new EventLog();
                    log.WriteEntry(msg.ToString() + " " +CurrentIndianTime());
                    return null;
                }
            }
        }

        public bool DeleteBlog(int? Id)
        {
            using(DatabaseContext db = new DatabaseContext())
            {
                blog = db.Blogs.Find(Id);

                if (blog == null)
                    return false;
                try
                {
                    db.Blogs.Remove(blog);
                    db.SaveChanges();
                    return true;
                }catch(Exception msg)
                {
                    EventLog log = new EventLog();
                    log.WriteEntry(msg.ToString() + " " + CurrentIndianTime());
                    return false;
                }
            }
        }
    }
}
