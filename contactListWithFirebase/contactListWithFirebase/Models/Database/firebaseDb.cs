using contactListWithFirebase.Models.Entities;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace contactListWithFirebase.Models.Database
{
    public class firebaseDb
    {
        IFirebaseConfig config = new FirebaseConfig()
        {
            AuthSecret = "ykYGYeyb8yG05QEgj4wU1BNGgTcTFmYa6fIe1gan",
            BasePath = "https://contactlistfirebasedb.firebaseio.com/"
        };

        IFirebaseClient client;

        public firebaseDb()
        {
            client = new FireSharp.FirebaseClient(config);
        }

        public HttpStatusCode addNewRecord(Contact contactData)
        {
            try
            {
                PushResponse response = client.Push("contactList", contactData);
                contactData.dbId = response.Result.name;
                return client.Set("contactList/" + contactData.dbId, contactData).StatusCode;
            }
            catch (Exception ex)
            {
                return HttpStatusCode.ExpectationFailed;
            }


        }

        public HttpStatusCode updateRecord(Contact contactData)
        {
            try
            {
                SetResponse response = client.Set<Contact>($"contactList/{contactData.dbId}", contactData);
                return response.StatusCode;
            }
            catch (Exception ex)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        public HttpStatusCode deleteRecord(string firebaseDbID)
        {
            try
            {
                FirebaseResponse response = client.Delete($"contactList/{firebaseDbID}");
                return response.StatusCode;
            }
            catch (Exception ex)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        public FirebaseResponse GetAllRecords()
        {
            FirebaseResponse response = client.Get("contactList");

            return response;

        }

        internal FirebaseResponse getRecordById(string firebaseDbID)
        {
            return client.Get($"contactList/{firebaseDbID}");
        }
    }
}