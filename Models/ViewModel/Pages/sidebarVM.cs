using Online_Shop.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Shop.Models.ViewModel.Pages
{
    public class sidebarVM
    {
        public sidebarVM()
        {
                
        }
        public sidebarVM(sidebarDTO row)
        {
            Id = row.Id;
            Body = row.Body;
        }
        public int Id { get; set; }
        [AllowHtml]
        public string Body { get; set; }
    }
}