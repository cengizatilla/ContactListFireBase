using contactListWithFirebase.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace contactListWithFirebase.Controllers
{
    public class ContactListController : Controller
    {
        // GET: ContactList
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult addNewRecord()
        {
            return View(new Contact());
        }

        [HttpPost]
        public ActionResult addNewRecord(Contact contactData)
        {
            Models.Services.contactListService contactListService = new Models.Services.contactListService();
            HttpStatusCode statusCode = contactListService.addNewRecord(new Models.Entities.Contact()
            {
                firstName = contactData.firstName,
                lastName = contactData.lastName,
                communicationValueI = contactData.communicationValueI,
                communicationValueII = contactData.communicationValueII,
                communicationValueIII = contactData.communicationValueIII,
                notes = contactData.notes
            });

            if (statusCode == HttpStatusCode.OK)
                return RedirectToAction("getAllRecords");
            else
            {
                return View(contactData);
            }
        }

        [HttpGet]
        public ActionResult getAllRecords()
        {
            Models.Services.contactListService contactListService = new Models.Services.contactListService();
            List<Contact> dataList = contactListService.GetAllRecords();
            return View(dataList);
        }

        public ActionResult recordDetailView(string firebaseDbID)
        {
            Models.Services.contactListService contactListService = new Models.Services.contactListService();
            Contact theRecord = contactListService.getRecordById(firebaseDbID);
            return View(theRecord);
        }

        [HttpPost]
        public ActionResult recordDetailUpdate(string firstName, string lastName, string communicationValueI, string communicationValueII, string communicationValueIII, string notes, string dbId)
        {
            Models.Services.contactListService contactListService = new Models.Services.contactListService();
            HttpStatusCode statusCode = contactListService.updateRecord(new Contact()
            {
                dbId = dbId,
                firstName = firstName,
                lastName = lastName,
                communicationValueI = communicationValueI,
                communicationValueII = communicationValueII,
                communicationValueIII = communicationValueIII,
                notes = notes
            });

            if (statusCode == HttpStatusCode.OK) ViewBag.message = "Record Updated";
            else ViewBag.message = "Update Data Error";

            Contact theRecord = contactListService.getRecordById(dbId);
            return RedirectToAction("recordDetailView", new { firebaseDbID = theRecord.dbId });

        }

        
        public ActionResult recordDetailDelete(string firebaseDbID)
        {
            Models.Services.contactListService contactListService = new Models.Services.contactListService();
            HttpStatusCode statusCode = contactListService.deleteRecord(firebaseDbID);

            return RedirectToAction("getAllRecords");
        }


    }
}