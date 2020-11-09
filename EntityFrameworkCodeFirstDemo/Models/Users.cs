using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EntityFrameworkCodeFirstDemo.Models
{
    public class User
    {
        public int id { get; set; }
        [Required]
        [StringLength(10, ErrorMessage ="Max 10 characters allowed")]
        public string userName { get; set; }
        [Required]
        [RegularExpression("([a-zA-Z ]+)")]

        public string firstName { get; set; }
        [RegularExpression("([a-zA-Z ]+)")]
        public string  lastName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string emailID { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateofJoin { get; set; }
        public int managerID { get; set; }
        public int roleID { get; set; }
    }

    public class UserDBContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }
    }



}