using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Online_Shop.Models.Data
{
    public class Db : DbContext
    {
        public DbSet<pageDTO> Pages{ get; set; }
        public DbSet<sidebarDTO> Sidebars { get; set; }
    }
}