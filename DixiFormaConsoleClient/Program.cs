using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DixiFormaConsoleClient
{
    class Program
    {/*
        static String register()
        {
            Console.WriteLine("Введите логин:");
            String login = Console.ReadLine().Trim().Split(' ')[0];
            Console.WriteLine("Введите пароль:");
            String password = Console.ReadLine().Trim().Split(' ')[0];
            User user = new User { Login = login, Password = password };

        }
        static String connect()
        {

        }
        static void incorrect()
        {
            Console.WriteLine("Некорректный ввод! Повторите попытку.");
        }
        static String auth()
        {
            Console.WriteLine("Клиент запущен.");
            Console.WriteLine("1) Регистрация\n2) Вход\n0) Завершить работу");
            var s = Console.ReadLine().Trim();
            switch (s)
            {
                case "0":
                    {
                        return null;
                    }
                case "1":
                    {
                        return register();
                    }
                case "2":
                    {
                        return connect();
                    }
            }
            incorrect();
            return auth();
        }*/
        static void Main(string[] args)
        {
            //var c = new Client();
            //if (auth() == null) {
                Console.WriteLine("Работа клиента завершена.");
                Console.ReadKey();
                return;
            //}
            String s = "run";
            while (s!=null)
            {
                

            }
        }
    }
}
