using Microsoft.Web.WebSockets;
using RESTful1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace RESTful1.Controllers
{
    public class ContactesController : ApiController
    {
        // 1 GET: api/contactes
        [HttpGet]
        [Route("api/contactes")]
        public HttpResponseMessage Get()
        {
            var contactes = ContactesRepository.GetAllContactes();
            if (contactes == null) { return Request.CreateResponse(HttpStatusCode.NotFound); }

            foreach (var c in contactes)
            {
                c.serializeMail = false;
                c.serializeTelefons = false;
            }

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, contactes);
            return response;
        }
        // 2 GET: api/contactesTot
        [HttpGet]
        [Route("api/contactesTot")]
        public HttpResponseMessage GetTot()
        {
            var contactes = ContactesRepository.GetAllContactes();
            if (contactes == null) { return Request.CreateResponse(HttpStatusCode.NotFound); }
            foreach (var c in contactes)
            {
                c.serializeMail = true;
                c.serializeTelefons = true;
            }

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, contactes);
            return response;
        }

        // 3 GET: api/contacte/5
        [HttpGet]
        [Route("api/contacte/{id?}")]
        public HttpResponseMessage GetContacte(int id)
        {
            var contacte = ContactesRepository.GetContacte(id);
            if(contacte == null) { return Request.CreateResponse(HttpStatusCode.NotFound); }
            contacte.serializeMail = false;
            contacte.serializeTelefons = false;
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, contacte);
            return response;
        }
        // 4 GET: api/contacte/5
        [HttpGet]
        [Route("api/contacteTot/{id?}")]
        public HttpResponseMessage GetContacteTot(int id)
        {
            var contacte = ContactesRepository.GetContacte(id);
            if (contacte == null) { return Request.CreateResponse(HttpStatusCode.NotFound); }
            contacte.serializeMail = true;
            contacte.serializeTelefons = true;
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, contacte);
            return response;
        }

        // 5 GET: api/contacte/string
        [HttpGet]
        [Route("api/contactes/{name:alpha}")]
        public HttpResponseMessage GetNom(string name)
        {
            var contactes = ContactesRepository.SearchContactesByName(name);
            if (contactes == null) { return Request.CreateResponse(HttpStatusCode.NotFound); }
            foreach (var c in contactes)
            {
                c.serializeMail = false;
                c.serializeTelefons = false;
            }
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, contactes);
            return response;
        }
        // 6 GET: api/contacte/string
        [HttpGet]
        [Route("api/contactesTot/{name:alpha}")]
        public HttpResponseMessage GetNomTot(string name)
        {
            var contactes = ContactesRepository.SearchContactesByName(name);
            if (contactes == null) { return Request.CreateResponse(HttpStatusCode.NotFound); }
            foreach (var c in contactes)
            {
                c.serializeMail = true;
                c.serializeTelefons = true;
            }
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, contactes);
            return response;
        }
        // 7 GET: api/telefonC/string
        [HttpGet]
        [Route("api/telefonC/{phone}")]
        public HttpResponseMessage GetPhone(string phone)
        {
            var phones = ContactesRepository.SearchContactesByPhone(phone);
            if (phones == null) { return Request.CreateResponse(HttpStatusCode.NotFound); }
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, phones);
            return response;
        }
        // 8 GET: api/emailC/string
        [HttpGet]
        [Route("api/emailC/{email}")]
        public HttpResponseMessage GetEmail(string email)
        {
            var emails = ContactesRepository.SearchContactesByEmail(email);
            if (emails == null) { return Request.CreateResponse(HttpStatusCode.NotFound); }
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, emails);
            return response;
        }

        // 9 PUT: api/contacte/5
        [HttpPut]
        [Route("api/contacte/{id?}")]
        public HttpResponseMessage PutC(int id, [FromBody]contacte val)
        {
            var contacte = ContactesRepository.UpdateContacte(id, val);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, contacte);
            return response;
        }
        // 10 PUT: api/telefon/5
        [HttpPut]
        [Route("api/telefon/{id?}")]
        public HttpResponseMessage PutT(int id, [FromBody]telefon val)
        {
            var tel = ContactesRepository.UpdatePhone(id, val);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, tel);
            return response;
        }
        // 11 PUT: api/email/5
        [HttpPut]
        [Route("api/email/{id?}")]
        public HttpResponseMessage PutE(int id, [FromBody]email val)
        {
            var email = ContactesRepository.UpdateMail(id, val);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, email);
            return response;
        }
        // 12 POST: api/contacte
        [HttpPost]
        [Route("api/contacte")]
        public HttpResponseMessage PostC([FromBody]contacte val)
        {
            var c = ContactesRepository.CreateContacte(val);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, c);
            return response;
        }
        // 13 POST: api/telefon
        [HttpPost]
        [Route("api/telefon")]
        public HttpResponseMessage PostT([FromBody]telefon val)
        {
            var t = ContactesRepository.CreatePhone(val);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, t);
            return response;
        }
        // 14 POST: api/email
        [HttpPost]
        [Route("api/email")]
        public HttpResponseMessage PostE([FromBody]email val)
        {
            var e = ContactesRepository.CreateMail(val);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, e);
            return response;
        }
        // 15 POST: api/contacteTot
        [HttpPost]
        [Route("api/contacteTot")]
        public HttpResponseMessage PostCTot([FromBody]contacte c)
        {
            var contactes = ContactesRepository.GetAllContactes();
            if (contactes == null) { return Request.CreateResponse(HttpStatusCode.NotFound); }

            c.serializeMail = true;
            c.serializeTelefons = true;

            var co = ContactesRepository.CreateContacte(c);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, co);
            return response;
        }

        // 16 DELETE: api/contacte/5
        [HttpDelete]
        [Route("api/contacte/{id?}")]
        public HttpResponseMessage DeleteC(int id)
        {
            var contacte = ContactesRepository.GetContacte(id);
            if (contacte == null) { return Request.CreateResponse(HttpStatusCode.NotFound); }
            if (ContactesRepository.contacteHasForeign(id)) { return Request.CreateResponse(HttpStatusCode.BadRequest);  }

            ContactesRepository.DeleteContacte(id);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
        // 17 DELETE: api/telefon/5
        [HttpDelete]
        [Route("api/telefon/{id?}")]
        public HttpResponseMessage DeleteT(int id)
        {
            var telefon = ContactesRepository.GetPhone(id);
            if (telefon == null) { return Request.CreateResponse(HttpStatusCode.NotFound); }

            ContactesRepository.DeletePhone(id);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
        // 18 DELETE: api/email/5
        [HttpDelete]
        [Route("api/email/{id?}")]
        public HttpResponseMessage DeleteE(int id)
        {
            var email = ContactesRepository.GetEmail(id);
            if (email == null) { return Request.CreateResponse(HttpStatusCode.NotFound); }

            ContactesRepository.DeleteEmail(id);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
        // 19 DELETE: api/contacteTot/5
        [HttpDelete]
        [Route("api/contacteTot/{id?}")]
        public HttpResponseMessage DeleteCTot(int id)
        {
            var contacte = ContactesRepository.GetContacte(id);
            if (contacte == null) { return Request.CreateResponse(HttpStatusCode.NotFound); }

            ContactesRepository.DeleteContacteTot(id);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        // WebSockets: Socket api call wss://host:port/api/websocket?nom={name}
        public HttpResponseMessage Get(string name)
        {
            HttpContext.Current.AcceptWebSocketRequest(new SocketHandler(name)); return new HttpResponseMessage(HttpStatusCode.SwitchingProtocols);
        }
        private class SocketHandler : WebSocketHandler
        {
            private static readonly WebSocketCollection Sockets = new WebSocketCollection();
            private static readonly List<String> usersOnline = new List<String>();

            private readonly string _nom;

            public SocketHandler(string nom)
            {
                _nom = nom;
            }

            public override void OnOpen()
            {
                // Quan es connecta un nou usuari: cal afegir el SocketHandler a la Collection, notificar a tothom la incorporació i donar-li la benvinguda
                Sockets.Add(this);
                usersOnline.Add(_nom);
                Sockets.Broadcast("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + this._nom + " joined the chat.");
                Sockets.Broadcast("*" + string.Join(",", usersOnline));
            }

            public override void OnMessage(string message)
            {
                // Quan un usuari envia un missatge, cal que tothom el rebi
                Sockets.Broadcast("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + this._nom + ": " + message);

            }

            public override void OnClose()
            {
                // Quan un usuari desconnecta, cal acomiadar-se'n, esborrar-ne el SocketHandler de la Collection i notificar a la resta que marxa
                this.Send("Adeu " + this._nom);
                Sockets.Remove(this);
                usersOnline.Remove(_nom);
                Sockets.Broadcast("[" + DateTime.Now.ToString("HH:mm:ss") + "] " + this._nom + " left the chat.");
                Sockets.Broadcast("*" + string.Join(",", usersOnline));

            }
        }
    }
}