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
    public class UsersController : ApiController
    {
        // GET: api/Users
        public IEnumerable<User> GetUsers()
        {
            return MessagingService.GetUsers();
        }

        // GET: api/Users/{username}
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(string id)
        {
            var user = await MessagingService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/{username}
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(string id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Username)
            {
                return BadRequest();
            }

            if (!UserExists(id))
            {
                return NotFound();
            }

            try
            {
                await MessagingService.UpdateUser(user);
            }
            catch (Exception)
            {
                return InternalServerError();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await MessagingService.AddUser(user);
            }
            catch (InvalidOperationException)
            {
                return Conflict();
            }

            return CreatedAtRoute("DefaultApi", new { id = user.Username }, user);
        }

        // DELETE: api/Users/{username}
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {
            try
            {
                var user = await MessagingService.DeleteUser(id);
                return Ok(user);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            
        }

        private bool UserExists(string id)
        {
            return MessagingService.UserExists(id);
        }
    }
}