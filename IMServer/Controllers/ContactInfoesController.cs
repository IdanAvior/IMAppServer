using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using IMAppServer;

namespace IMServer.Controllers
{
    public class ContactInfoesController : ApiController
    {
        // GET: api/ContactInfoes
        public IEnumerable<ContactInfo> GetContactInfoes()
        {
            return MessagingService.GetContactInfoes();
        }

        // GET: api/ContactInfoes/{contactName}/{username}
        [Route("api/contactinfoes/{contactName}/{username}")]
        [ResponseType(typeof(ContactInfo))]
        public async Task<IHttpActionResult> GetContactInfo(string contactName, string username)
        {
            var contactInfo = await MessagingService.GetContactInfo(contactName, username);
            if (contactInfo == null)
            {
                return NotFound();
            }

            return Ok(contactInfo);
        }

        // PUT: api/ContactInfoes/{contactName}/{username}
        [Route("api/contactinfoes/{contactName}/{username}")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutContactInfo(string contactName, string username, ContactInfo contactInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (contactName != contactInfo.ContactUsername || username != contactInfo.UserId)
            {
                return BadRequest();
            }

            try
            {
                await MessagingService.UpdateContactInfo(contactInfo);
            }
            catch (InvalidOperationException)
            {
                if (!ContactInfoExists(contactName, username))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ContactInfoes
        [ResponseType(typeof(ContactInfo))]
        public async Task<IHttpActionResult> PostContactInfo(ContactInfo contactInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await MessagingService.AddContactInfo(contactInfo);
            }
            catch (InvalidOperationException)
            {
                if (ContactInfoExists(contactInfo.ContactUsername, contactInfo.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id1 = contactInfo.ContactUsername, id2 = contactInfo.UserId }, contactInfo);
        }

        // DELETE: api/ContactInfoes/{contactName}/{username}
        [Route("api/contactinfoes/{contactName}/{username}")]
        [ResponseType(typeof(ContactInfo))]
        public async Task<IHttpActionResult> DeleteContactInfo(string contactName, string username)
        {
            try
            {
                var contactInfo = await MessagingService.DeleteContactInfo(contactName, username);
                return Ok(contactInfo);
            }
            catch(InvalidOperationException)
            {
                return NotFound();
            }
            
        }

        private bool ContactInfoExists(string contactName, string userId)
        {
            return MessagingService.ContactInfoExists(contactName, userId);
        }
    }
}