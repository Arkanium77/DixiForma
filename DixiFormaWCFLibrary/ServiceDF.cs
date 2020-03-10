using DixiFormaWCFLibrary.Model;
using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DixiFormaWCFLibrary
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceDF : IServiceDF
    {
        private Dictionary<User, OperationContext> sessions;
        private readonly DbWorker db = new DbWorker();

        public void Disconnect(User user)
        {
            if (sessions.ContainsKey(user))
            {
                sessions.Remove(user);
                SendMsg(new Message { User = db.System, Text = $"Пользователь {user.Login} покинул чат!", SendingTime = DateTime.Now });
            }
        }

        public bool Register(User user)
        {
            if (!db.checkRegister(user))
            {
                db.registerNewUser(user);
                return true;
            }
            return false;
        }

        public void SendMsg(Message message)
        {
            foreach (var item in sessions.Values)
            {
                item.GetCallbackChannel<IServiceDFCallback>().MsgCallback(message);
            }
        }

        public bool Connect(User user)
        {
            Disconnect(user);
            sessions.Add(user, OperationContext.Current);

            SendMsg(new Message { User = db.System, Text = $"Пользователь {user.Login} подключился к чату!", SendingTime=DateTime.Now});
            return true;
        }
    }
}
