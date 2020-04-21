using contactListWithFirebase.Models.Entities;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace contactListWithFirebase.Models.Services
{
    public class contactListService
    {
        Database.firebaseDb firebaseDatabase;
        public contactListService()
        {
            firebaseDatabase = new Database.firebaseDb();
        }

        public HttpStatusCode addNewRecord(Contact contactData)
        {
            return firebaseDatabase.addNewRecord(contactData);
        }

        public HttpStatusCode updateRecord(Contact contactData)
        {
            return firebaseDatabase.updateRecord(contactData);
        }

        public List<Contact> GetAllRecords()
        {
            List<Contact> contactList = new List<Contact>();
            try
            {
                FirebaseResponse result = firebaseDatabase.GetAllRecords();
                var fireBaseDataSoft = JsonConvert.DeserializeObject<dynamic>(result.Body);
                if (fireBaseDataSoft == null) return new List<Contact>() { };

                foreach (var item in fireBaseDataSoft)
                {
                    contactList.Add(JsonConvert.DeserializeObject<Contact>(((JProperty)item).Value.ToString()));
                }

                return contactList;
            }
            catch (Exception ex)
            {
                return new List<Contact>() { };
            }
        }

        internal Contact getRecordById(string firebaseDbID)
        {
            Contact theRecord = new Contact();

            if (string.IsNullOrEmpty(firebaseDbID)) return new Contact();

            FirebaseResponse result = firebaseDatabase.getRecordById(firebaseDbID);
            if (result == null) return new Contact();

            theRecord = result.ResultAs<Contact>();
            return theRecord;
        }

        internal HttpStatusCode deleteRecord(string firebaseDbID)
        {
            if (string.IsNullOrEmpty(firebaseDbID)) return HttpStatusCode.BadRequest;
            return firebaseDatabase.deleteRecord(firebaseDbID);
        }
    }
}