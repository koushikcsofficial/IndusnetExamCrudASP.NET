using DataAccess;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class CategoryLogics : General
    {
        private Category category;
        public Category CreateCategory(string category_Name)
        {
            category = new Category();
            using (DatabaseContext db = new DatabaseContext())
            {
                if (db.Categories.Any(c => c.Category_Name.Equals(category_Name, StringComparison.OrdinalIgnoreCase)))
                {
                    return null;
                }
                else
                {
                    category.Category_Name = category_Name.ToString().Trim();
                    try
                    {
                        db.Categories.Add(category);
                        db.SaveChanges();
                        return category;
                    }
                    catch (Exception msg)
                    {
                        EventLog log = new EventLog();
                        log.WriteEntry(msg.ToString() + " " + CurrentIndianTime());
                        return null;
                    }

                }
            }

        }
        public List<Category> GetCategories()
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                return db.Categories.ToList();
            }
        }
        public Category GetCategory(int? Id)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                return db.Categories.Find(Id);
            }
        }
        public Category EditCategory(int? Id, string category_Name)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                if(db.Categories.Any(c=>c.Category_Name.Equals(category_Name, StringComparison.OrdinalIgnoreCase)))
                {
                    return null;
                }
                category = db.Categories.Find(Id);
                if (category == null)
                    return null;
                category.Category_Name = category_Name.ToString().Trim();
                try
                {
                    db.SaveChanges();
                    return category;
                }
                catch (Exception msg)
                {
                    EventLog log = new EventLog();
                    log.WriteEntry(msg.ToString() + " " + CurrentIndianTime());
                    return null;
                }
            }
        }
        public bool DeleteCategory(int? Id)
        {
            using (DatabaseContext db = new DatabaseContext())
            {
                category = db.Categories.Find(Id);
                if (category == null)
                    return false;
                try
                {
                    db.Categories.Remove(category);
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
