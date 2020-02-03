using Online_Shop.Models.Data;
using Online_Shop.Models.ViewModel.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Shop.Areas.Admin.Controllers
{
    public class PagesController : Controller
    {
        // GET: Admin/Pages
        public ActionResult Index()
        {
            //declare list of pageVMs
            List<pageVM> pagesList;

            
            using (Db db = new Db())
            {
                //init list
                pagesList = db.Pages.ToArray().OrderBy(x => x.Sorting).Select(x=> new pageVM(x)).ToList();
            }

            //return view with list
            return View(pagesList);
        }
        // GET: Admin/Pages/AddPage
        [HttpGet]
        public ActionResult AddPage()
        {

            return View();
        }
        // Post: Admin/Pages/AddPage
        [HttpPost]
        public ActionResult AddPage(pageVM model)
        {
            //Check model state
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (Db db = new Db())
            {
                //Declare Slug
                string slug;
                //Init pageDTO
                pageDTO dto = new pageDTO();
                //DTO title
                dto.Title = model.Title;
                //Check for and Set Slug if needed
                if (string.IsNullOrWhiteSpace(model.Slug))
                {
                    slug = model.Title.Replace(" ", "-").ToLower(); 
                }
                else
                {
                    slug = model.Slug.Replace(" ", "-").ToLower();
                }
                //Making sure title & slug unique
                if (db.Pages.Any(x=>x.Title== model.Title) || db.Pages.Any(x=> x.Slug == model.Slug))
                {
                    ModelState.AddModelError("","The Title or SLug already exists");
                    return View(model);
                }
                //DTO the rest
                dto.Slug = slug;
                dto.Body = model.Body;
                dto.HasSidebar = model.HasSidebar;
                dto.Sorting = 100;

                //Save DTO
                db.Pages.Add(dto);
                db.SaveChanges();
            }
            //Save TempData Message
            TempData["SM"] = "Page has been added!";
            //Redirect

            return RedirectToAction("AddPage");
        }
        
        
        //GET: Admin/Pages/EditPage/ID
        public ActionResult EditPage(int id)
        {
            //Declare PageVM
            pageVM model;
            using(Db db = new Db())
            {
                //Get Page 
                pageDTO dto = db.Pages.Find(id);

                //Confirm Page exist
                if(dto == null)
                {
                    return Content("The Page does not exist");
                }

                //Init PageVM
                model = new pageVM(dto);
            }
            //Return View With Model

            return View(model);
        }


    }
}