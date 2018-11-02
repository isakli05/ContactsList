using ContactsList.Models.VM;
using DAL.ORM.Entity;
using DAL.ORM.Context;
using DAL.ORM.Entity.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactsList.Controllers
{
    using ContactsList.ImageUploader;
    public class ContactController : Controller
    {
        ProjectContext _service;

        public ContactController()
        {
            _service = new ProjectContext();
        }

        public ActionResult ContactDetail(int? id)
        {
            if (id == null) return RedirectToAction("List", "Contact");
           
            Contact Details = _service.Contacts.Where(x=>x.Status==Status.Active|| x.Status==Status.Updated).FirstOrDefault();

            if (Status.Deleted == Details.Status)
            {
                ViewBag.message = "Böyle bir ID'ye ait Kayıt bulunamadı !!!";
                return View();
            }
            ContactVM model = new ContactVM();

            model.ID = Details.ID;
            model.Name = Details.Name;
            model.SurName = Details.SurName;
            model.Company = Details.Company;
            model.Phone = Details.Phone;
            model.Mail = Details.Mail;
            model.ImagePath = Details.ImagePath;
            model.Status = Details.Status;
            return View(model);
        }

        [HttpGet]
        public ActionResult List()
        {
            return View(_service.Contacts.Where(x => x.Status == Status.Active || x.Status == Status.Updated).ToList());
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Contact data, HttpPostedFileBase Image)
        {

            data.ImagePath = ImageUploader.UploadImage("~/Uploads/", Image);


            if (data.ImagePath == "0" || data.ImagePath == "1" || data.ImagePath == "2")
            {
                data.ImagePath = "/Uploads/defaultProfile.png";
            }
            _service.Contacts.Add(data);
            _service.SaveChanges();
            return RedirectToAction("ContactDetail", "Contact");
        }

       [HttpGet]
      public ActionResult Update(int? id)
      {
          if (id == null) return RedirectToAction("List", "Contact");

          Contact updated = _service.Contacts.Find(id);
          ContactVM model = new ContactVM();

          model.ID = updated.ID;
          model.Name = updated.Name;
          model.SurName = updated.SurName;
          model.Company = updated.Company;
          model.Phone = updated.Phone;
          model.Mail = updated.Mail;
          model.ImagePath = updated.ImagePath;
          model.Status = updated.Status;
          return View(model);
      }

        [HttpPost]
        public ActionResult Update(ContactVM data, HttpPostedFileBase Image)
        {
            Contact updated = _service.Contacts.Find(data.ID);

            data.ImagePath = ImageUploader.UploadImage("/Uploads/", Image);

            if (data.ImagePath != "0" && data.ImagePath != "1" && data.ImagePath != "2")
            {
                updated.ImagePath = data.ImagePath;
            }

            updated.Name = data.Name;
            updated.SurName = data.SurName;
            updated.Company = data.Company;
            updated.Phone = data.Phone;
            updated.Mail = data.Mail;
            updated.Status = Status.Updated;
            _service.SaveChanges();
            return RedirectToAction("ContactDetail", "Contact");
            

        }


        public ActionResult Delete(int? id)
        {
            if (id == null) return RedirectToAction("List", "Contact");

            Contact deleted = _service.Contacts.Find(id);

            // Database den Silmek için
            //_service.Contacts.Remove(delete);

            // Status değiştirme
            deleted.Status = Status.Deleted;
            _service.SaveChanges();
            return Redirect("/Contact/List");
        }
    }
}