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
    public class MessagesController : ApiController
    {
        // GET: api/Messages
        public IEnumerable<Message> GetMessages()
        {
            return MessagingService.GetMessages();
        }

        // GET: api/Messages/{id}
        [ResponseType(typeof(Message))]
        public async Task<IHttpActionResult> GetMessage(int id)
        {
            var message = await MessagingService.GetMessage(id);
            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        // PUT: api/Messages/{id}
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMessage(int id, Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != message.Id)
            {
                return BadRequest();
            }

            try
            {
                await MessagingService.UpdateMessage(message);
            }
            catch (InvalidOperationException)
            {
                if (!MessageExists(id))
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

        // POST: api/Messages
        [ResponseType(typeof(Message))]
        public async Task<IHttpActionResult> PostMessage(Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await MessagingService.AddMessage(message);

            return CreatedAtRoute("DefaultApi", new { id = message.Id }, message);
        }

        // DELETE: api/Messages/{id}
        [ResponseType(typeof(Message))]
        public async Task<IHttpActionResult> DeleteMessage(int id)
        {
            try
            {
                var message = await MessagingService.DeleteMessage(id);
                return Ok(message);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            
        }

        private bool MessageExists(int id)
        {
            return MessagingService.MessageExists(id);
        }
    }
}