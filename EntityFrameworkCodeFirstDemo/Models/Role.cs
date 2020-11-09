using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntityFrameworkCodeFirstDemo.Models
{
    public class Role
    {
        public int id { get; set; }
        public string roleName { get; set; }
        public List<User> users { get; set; }
    }
}