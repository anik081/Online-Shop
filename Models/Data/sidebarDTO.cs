using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Online_Shop.Models.Data
{
    [Table("sidebar")]
    public class sidebarDTO
    {
        [Key]
        public int Id { get; set; }
        public string Body { get; set; }
    }
}