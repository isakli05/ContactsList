using DAL.ORM.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactsList.Models.VM
{
    public class ContactVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Company { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public string ImagePath { get; set; }
        public Status Status { get; set; }
    }
}