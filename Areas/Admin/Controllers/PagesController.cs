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
        [HttpGet]
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
        //POST: Admin/Pages/EditPage/ID
        [HttpPost]
        public ActionResult EditPage(pageVM model)
        {
            //Get model state
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (Db db = new Db())
            {
                //Get Id
                int Id = model.Id;
                //Declare Slug
                string slug ="home";

                //Get the page
                pageDTO dto = db.Pages.Find(Id);
                //DTO title
                dto.Title = model.Title;
                //Check slug and set it if needed
                if (model.Slug != "home")
                {
                    if (string.IsNullOrWhiteSpace(model.Slug))
                    {
                        slug = model.Title.Replace(" ", "-").ToLower();
                    }
                    else
                    {
                        slug = model.Slug.Replace(" ", "-").ToLower();

                    }
                }
                //Check if slug and title unique
                if(db.Pages.Where(x=> x.Id != Id).Any(x=> x.Title == model.Title)|| db.Pages.Where(x => x.Id != Id).Any(x => x.Slug == slug))
                {
                    ModelState.AddModelError("", "The title or slug already exists");
                    return View(model);
                }
                //dto the rest
                dto.Slug = slug;
                dto.Body = model.Body;
                dto.HasSidebar = model.HasSidebar;
                //save dto
                db.SaveChanges();
            }
            //save tempdata message
            TempData["SM"] = "The page has been edited!!!";
            //Redirect
            return RedirectToAction("EditPage");
        }
        //POST: Admin/Pages/PageDetails
        public ActionResult PageDetails(int id)


        {
            //Declare PageVM
            pageVM model;

            using (Db db = new Db())
            {
                //Get the Page
                pageDTO dto = db.Pages.Find(id);

                //Confirm Page Exists
                if(dto == null)
                {
                    return Content("The Page doesn't exist");
                }
                model =new pageVM(dto);

                //Init PageVM
            }
            //Return View with model 
            return View(model);
        }
        //GET: Admin/Pages/DeletePage
        public ActionResult DeletePage(int id)


        {
            using (Db db = new Db())
            {

                //get page
                pageDTO dto = db.Pages.Find(id);

                //remove page
                db.Pages.Remove(dto);

                // save
                db.SaveChanges();
            }
            //redirect

            return RedirectToAction("Index");
        }

        //POST: Admin/Pages/ReorderPage
        [HttpPost]
        public void  ReorderPage(int[] id)


        {
            using (Db db = new Db())
            {
                //set initial count
                int count = 1;
                //declare pagDTO
                pageDTO dto;
                //Set Sorting for each page
                foreach (var pageid in id)
                {
                    dto = db.Pages.Find(pageid);
                    dto.Sorting = count;
                    db.SaveChanges();
                    count++;
                }
            }
        }
        [HttpGet]
        //GET: Admin/Pages/EditSidebar
        public ActionResult EditSidebar()
        {
            //declare model
            sidebarVM model;
            using (Db db = new Db())
            {
                //get the dto
                sidebarDTO dto = db.Sidebars.Find(1);
                // init model
                model = new sidebarVM(dto);
            }
            //return view with model
            return View(model);
        }
        [HttpPost]
        //Post: Admin/Pages/EditSidebar
        public ActionResult EditSidebar(sidebarVM model)
        {
            using (Db db = new Db())
            {


                //get dto
                sidebarDTO dto = db.Sidebars.Find(1);
                //dto the body
                dto.Body = model.Body;
                //save
                db.SaveChanges();
                //temp message
                TempData["SM"] = "Side bas has been edited";
            }
            //redirect

            return RedirectToAction("EditSidebar");
        }


    }
}