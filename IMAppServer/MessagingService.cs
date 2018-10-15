using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMAppServer
{
    public class MessagingService
    {
        public static IEnumerable<User> GetUsers()
        {
            using (var db = new MessageContext())
            {
                return db.Users.ToList();
            }
        }

        public static IEnumerable<Message> GetMessages()
        {
            using (var db = new MessageContext())
            {
                return db.Messages.ToList();
            }
        }

        public static IEnumerable<ContactInfo> GetContactInfoes()
        {
            using (var db = new MessageContext())
            {
                return db.ContactInfoes.ToList();
            }
        }

        public static async Task<User> GetUser(string username)
        {
            using (var db = new MessageContext())
            {
                var user = await db.Users.FindAsync(username);
                return user;
            }
        }

        public static async Task<Message> GetMessage(int id)
        {
            using (var db = new MessageContext())
            {
                return await db.Messages.FindAsync(id);
            }
        }

        public static async Task<ContactInfo> GetContactInfo(string contactUsername, string userId)
        {
            using (var db = new MessageContext())
            {
                return await db.ContactInfoes.FindAsync(contactUsername, userId);
            }
        }

        public static async Task UpdateUser(User user)
        {
            using (var db = new MessageContext())
            {
                var userInDb = db.Users.Where(c => c.Username == user.Username).Include(p => p.Contacts).SingleOrDefault();

            if (userInDb != null) {
                    db.Entry(userInDb).CurrentValues.SetValues(user);

                    foreach (var contact in userInDb.Contacts)
                    {
                        if (!user.Contacts.Any(c => c.ContactUsername == contact.ContactUsername && c.UserId == contact.UserId))
                            db.ContactInfoes.Remove(contact);
                    }

                    foreach (var contact in user.Contacts)
                    {
                        var contactInDb = userInDb.Contacts.Where(c => c.ContactUsername == contact.ContactUsername && c.UserId == contact.UserId).SingleOrDefault();

                        if (contactInDb != null)
                            db.Entry(contactInDb).CurrentValues.SetValues(contact);
                        else
                        {
                            var newContact = new ContactInfo { ContactUsername = contact.ContactUsername, UserId = contact.UserId };
                            userInDb.Contacts.Add(newContact);
                        }
                    }

                    await db.SaveChangesAsync();
            }
                await db.SaveChangesAsync();
            }
        }

        public static async Task UpdateMessage(Message message)
        {
            using (var db = new MessageContext())
            {
                var msgInDb = await db.Messages.FindAsync(message.Id);
                if (msgInDb == null)
                    throw new InvalidOperationException("Specified message does not exist in the database");
                msgInDb = message;
                await db.SaveChangesAsync();
            }
        }

        public static async Task UpdateContactInfo(ContactInfo contactInfo)
        {
            using (var db = new MessageContext())
            {
                var contactInfoInDb = await db.ContactInfoes.FindAsync(contactInfo.ContactUsername, contactInfo.UserId);
                if (contactInfoInDb == null)
                    throw new InvalidOperationException("Contact does not exist");
                contactInfoInDb = contactInfo;
                await db.SaveChangesAsync();
            }
        }

        public static async Task AddUser(User user)
        {
            using (var db = new MessageContext())
            {
                if (UserExists(user.Username))
                    throw new InvalidOperationException("Username already taken");
                db.Users.Add(user);
                await db.SaveChangesAsync();
            }
        }

        public static async Task AddMessage(Message message)
        {
            using (var db = new MessageContext())
            {
                if (MessageExists(message.Id))
                    throw new InvalidOperationException("A message with the same ID already exists");
                db.Messages.Add(message);
                await db.SaveChangesAsync();
            }
        }

        public static async Task AddContactInfo(ContactInfo contactInfo)
        {
            using (var db = new MessageContext())
            {
                if (ContactInfoExists(contactInfo.ContactUsername, contactInfo.UserId))
                    throw new InvalidOperationException("Contact already exists");
                db.ContactInfoes.Add(contactInfo);
                await db.SaveChangesAsync();
            }
        }

        public static async Task<User> DeleteUser(string id)
        {
            using (var db = new MessageContext())
            {
                var userToDelete = await db.Users.FindAsync(id);
                if (userToDelete == null)
                    throw new InvalidOperationException("Specified user does not exist in the database");
                db.Users.Remove(userToDelete);
                foreach (var contactInfo in db.ContactInfoes)
                    if (contactInfo.UserId == id || contactInfo.ContactUsername == id)
                        db.ContactInfoes.Remove(contactInfo);
                await db.SaveChangesAsync();
                return userToDelete;
            }
        }

        public static async Task<Message> DeleteMessage(int id)
        {
            using (var db = new MessageContext())
            {
                var msgToDelete = await db.Messages.FindAsync(id);
                if (msgToDelete == null)
                    throw new InvalidOperationException("Specified message does not exist in the database");
                db.Messages.Remove(msgToDelete);
                await db.SaveChangesAsync();
                return msgToDelete;
            }
        }

        public static async Task<ContactInfo> DeleteContactInfo(string id1, string id2)
        {
            using (var db = new MessageContext())
            {
                var contactInfoToDelete = await db.ContactInfoes.FindAsync(id1, id2);
                if (contactInfoToDelete == null)
                    throw new InvalidOperationException("Specified contact does not exist");
                db.ContactInfoes.Remove(contactInfoToDelete);
                await db.SaveChangesAsync();
                return contactInfoToDelete;
            }
        }

        public static bool UserExists(string username)
        {
            using (var db = new MessageContext())
            {
                return db.Users.Count(c => c.Username == username) > 0;
            }
        }

        public static bool MessageExists(int id)
        {
            using (var db = new MessageContext())
            {
                return db.Messages.Count(c => c.Id == id) > 0;
            }
        }

        public static bool ContactInfoExists(string id1, string id2)
        {
            using (var db = new MessageContext())
            {
                return db.ContactInfoes.Count(c => c.ContactUsername == id1 && c.UserId == id2) > 0;
            }
        }

        private static bool ContactExists(User user, ContactInfo contact)
        {
            using (var db = new MessageContext())
            {
                return user.Contacts.Count(c => c.ContactUsername == contact.ContactUsername) > 0;
            }
        }

        private static async Task<bool> ContactExists(string username, ContactInfo contact)
        {
            using (var db = new MessageContext())
            {
                var userInDb = await db.Users.FindAsync(username);
                return userInDb.Contacts.Count(c => c.ContactUsername == contact.ContactUsername) > 0;
            }
        }

    }
}
