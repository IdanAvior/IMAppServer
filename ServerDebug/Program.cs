using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMAppServer;
using IMServer;
using System.Net.Http;
using Newtonsoft.Json;

namespace ServerDebug
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (var c = new HttpClient())
            //{
            //    //var user1 = new User { Username = "a", Password = "a", FirstName = "a", LastName = "a" };
            //    //var user2 = new User { Username = "b", Password = "b", FirstName = "b", LastName = "b" };

            //        var res1 = c.GetStringAsync("http://localhost:50354/api/users/a").Result;
            //        var res2 = c.GetStringAsync("http://localhost:50354/api/users/b").Result;

            //        var user1 = JsonConvert.DeserializeObject<User>(res1);
            //        var user2 = JsonConvert.DeserializeObject<User>(res2);

            //    user1.Contacts.Add(new ContactInfo { UserId = user1.Username, ContactUsername = user2.Username });
            //    user2.Contacts.Add(new ContactInfo { UserId = user2.Username, ContactUsername = user1.Username });

            //    //    var message1 = new Message { Sender = user1.Username, Receiver = user2.Username, TimeSent = DateTime.Now, Content = "Hello" };
            //    //    var message2 = new Message { Sender = user2.Username, Receiver = user1.Username, TimeSent = DateTime.Now, Content = "Hi" };
            //    //    var msgser = JsonConvert.SerializeObject(message1);
            //    //    var msgser2 = JsonConvert.SerializeObject(message2);
            //    //    var msgcont = new StringContent(msgser, Encoding.UTF8, "application/json");
            //    //    var msgcont2 = new StringContent(msgser2, Encoding.UTF8, "application/json");
            //    //    var msgres = c.PostAsync("http://localhost:50354/api/messages", msgcont).Result;
            //    //    var msgres2 = c.PostAsync("http://localhost:50354/api/messages", msgcont2).Result;
            //    //    Console.WriteLine(msgres);
            //    //    Console.WriteLine(msgres2);
            //    //    //user2.Contacts.Add(new ContactInfo { UserId = user2.Username, ContactUsername = user1.Username });

            //    //    //    //var ci1 = new ContactInfo { ContactUsername = user2.Username };
            //    //    //    //user1.Contacts.Add(ci1);

            //    var ser1 = JsonConvert.SerializeObject(user1);
            //    var ser2 = JsonConvert.SerializeObject(user2);

            //    ////    //Console.WriteLine(ser1);

            //        var cont1 = new StringContent(ser1, Encoding.UTF8, "application/json");
            //        var cont2 = new StringContent(ser2, Encoding.UTF8, "application/json");

            //    //var res1 = c.PostAsync("http://localhost:50354/api/users", cont1).Result;
            //    //var res2 = c.PostAsync("http://localhost:50354/api/users", cont2).Result;

            //    //Console.WriteLine(res1);
            //    //Console.WriteLine(res2);

            //    //var result1 = c.PutAsync("http://localhost:50354/api/users/a", cont1).Result;
            //    //var result2 = c.PutAsync("http://localhost:50354/api/users/b", cont2).Result;

            //    var result = c.DeleteAsync("http://localhost:50354/api/users/a").Result;
            //    Console.WriteLine(result);


            //    //Console.WriteLine(result1);
            //    //Console.WriteLine(result2);

            //    ////    //var ser3 = JsonConvert.SerializeObject(ci1);
            //    ////    //var cont3 = new StringContent(ser3, Encoding.UTF8, "application/json");
            //    ////    //var res3 = c.PostAsync("http://localhost:50354/api/contactinfoes", cont3).Result;
            //    ////    //Console.WriteLine(res3);
            //    ////    //Console.WriteLine(res2);
            //}
            //using (var db = new MessagingContext())
            //{
            //    var user1 = db.Users.Find("a");
            //    var user2 = db.Users.Find("b");
            //    user2.Contacts.Add(new ContactInfo { ContactUsername = user1.Username, UserId = user2.Username});
            //    user1.Contacts.Add(new ContactInfo { ContactUsername = user2.Username, UserId = user1.Username });
            //    //db.Users.Add(user2);
            //    //var user1 = new User { Username = "a", Password = "a", FirstName = "a", LastName = "a" };
            //    //var user2 = new User { Username = "b", Password = "b", FirstName = "b", LastName = "b" };
            //    //db.Users.Add(user1);
            //    //db.Users.Add(user2);
            //    db.SaveChanges();
            //}

        }
    }
}
