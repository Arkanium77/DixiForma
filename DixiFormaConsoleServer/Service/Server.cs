using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Service
{

    public class Server
    {
        IPEndPoint ipPoint;
        Socket listenSocket;
        DbWorker db;


        /// <summary>
        /// Создание сервера
        /// </summary>
        /// <param name="adress">IP адрес</param>
        /// <param name="port">Порт</param>
        /// <param name="backlog">Максимальная длина очереди ожидающих подключений</param>
        public Server(String address="127.0.0.1", int port=8080,int backlog=10)
        {
            ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
            db = new DbWorker();
            // создаем сокет
            listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                // связываем сокет с локальной точкой, по которой будем принимать данные
                listenSocket.Bind(ipPoint);

                // начинаем прослушивание
                listenSocket.Listen(backlog);
                
                Console.WriteLine("Сервер запущен. Ожидание подключений...");

                Process();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //Console.WriteLine("End Of Work");
        }
        public void Process()
        {
            while (true)
            {
                Socket handler = listenSocket.Accept();
                // получаем сообщение
                StringBuilder builder = new StringBuilder();
                int bytes = 0; // количество полученных байтов
                byte[] data = new byte[256]; // буфер для получаемых данных

                do
                {
                    bytes = handler.Receive(data); //получение переданных данных

                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (handler.Available > 0);
                
                var obj = JsonConvert.DeserializeObject(builder.ToString());
                if (obj.GetType().Name == "Message")
                {

                }
                Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());

                // отправляем ответ
                string message = "ваше сообщение доставлено";
                data = Encoding.Unicode.GetBytes(message);
                handler.Send(data);//посылаем данные
                                   // закрываем сокет
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }

        }
    }

}
