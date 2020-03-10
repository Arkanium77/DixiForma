using LiteDB;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BCrypt.Net.BCrypt;


namespace Service
{
    public class DbWorker: IDisposable
    {
        private readonly LiteDatabase db;
        public DbWorker(String con = @"MyData.db")
        {
            db = new LiteDatabase(con);
        }

        public bool isUserExist(User user)
        {
            User usr = db.GetCollection<User>("Users").FindById(user.Login);
            if (usr == null)
            {
                return false;
            }
            return true;
        }
        public bool checkRegister(User user)
        {
            if (isUserExist(user))
            {
                User usr = db.GetCollection<User>("Users").FindById(user.Login);
                if (user.Login == usr.Login && Verify(user.Password,usr.Password))
                {
                    return true;
                }
            }
            return false;
        }
   
        public void registerNewUser(User user)
        {
            if (isUserExist(user))
            {
                throw new UserAlreadyExistException();  
            }
            db.GetCollection<User>("Users").Insert(new User { Login = user.Login, Password = HashPassword(user.Password) });
            db.Commit();
        }
        public void deleteUser(User user)
        {
            if (isUserExist(user))
            {
                var f = db.GetCollection<User>("Users").Delete(user.Login);
                if (f)
                {
                    var i = db.GetCollection<Message>("Messages").DeleteMany(x => x.User.Login==user.Login);
                    Console.WriteLine(i);
                }
                db.Commit();
            }
            
        }
        public List<Message> getLastMessages(Int32 count = 50)
        {
            var msges=db.GetCollection<Message>("Messages").FindAll().OrderBy(x=>x.SendingTime).ToList();
            int index = msges.Count - count - 1;
            if (index < 0) index = 0;
            try
            {
                msges = msges.GetRange(index, count);
            }
            catch (ArgumentException e)
            {
                msges.Insert(0, new Message { User = new User { Login = "System" }, Text = "Начало переписки." });
            }
            return msges;
        }

        public void saveMessage(Message message)
        {
            db.GetCollection<Message>("Messages").Insert(message);
            db.Commit();
        }

        public void Dispose()
        {
            db.Commit();
            db.Dispose();
        }

        public void Drop(String s)
        {
            db.DropCollection(s);
        }
        public void DropDB()
        {
            foreach(String s in db.GetCollectionNames())
            {
                db.DropCollection(s);
            }
        }
        public String ShowDB()
        {
            String s = "Database:\n";
            foreach (User c in db.GetCollection<User>("Users").FindAll())
            {
                s += c.ToString() + "\n";
            }
            s += "\n\n";
            foreach (Message c in db.GetCollection<Message>("Messages").FindAll())
            {
                s += c.ToString() + "\n";
            }
            return s;
        }

        ~DbWorker()
        {
            db.Commit();
            db.Dispose();
        }
    }
}
