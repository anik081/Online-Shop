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
        public ActionResult AddPage()
        {

            return View();
        }
    }
}