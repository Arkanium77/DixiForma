using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DixiFormaWCFLibrary
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IServiceDF" в коде и файле конфигурации.
    [ServiceContract(CallbackContract = typeof(IServiceDFCallback))]
    public interface IServiceDF
    {
        [OperationContract]
        bool Connect(User user);

        [OperationContract]
        bool Register(User user);

        [OperationContract]
        void Disconnect(User user);

        [OperationContract(IsOneWay = true)]
        void SendMsg(Message message);

    }

    public interface IServiceDFCallback
    {
        [OperationContract(IsOneWay = true)]
        void MsgCallback(Message message);
    }
}
