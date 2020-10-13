using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bank.Models
{
    public class Bank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MFO { get; set; }

        public ICollection<User> Users { get; set; }

        public Bank()
        {
            Users = new List<User>();
        }
    }
}