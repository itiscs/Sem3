using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace HttpList
{
    class Program
    {
        public static string GetRequestPostData(HttpListenerRequest request)
        {
            if (!request.HasEntityBody)
            {
                return null;
            }
            using (System.IO.Stream body = request.InputStream) // here we have data
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(body, request.ContentEncoding))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        static void Main(string[] args)
        {
            HttpListener listener = new HttpListener();
            // установка адресов прослушки
            listener.Prefixes.Add("http://localhost:8888/connection/");
            listener.Prefixes.Add("http://localhost:8888/connection2/");
            listener.Start();
            Console.WriteLine("Ожидание подключений...");
            // метод GetContext блокирует текущий поток, ожидая получение запроса 

            while (true)
            {
                HttpListenerContext context = listener.GetContext();

                HttpListenerRequest request = context.Request;
                Cookie cook = request.Cookies["FirstName"];

                HttpListenerResponse response = context.Response;
                string responseStr;

                if (cook != null) //если есть куки, обращаемся к пользователю
                {
                    responseStr = $"<html><head></head><body>Hello, {cook.Value} </body></html>";
                }
                else
                {
                    
                    string postData = GetRequestPostData(request);

                    Dictionary<string, string> pd = new Dictionary<string, string>();
                    Console.WriteLine("PostData:");
                    if (postData != null)    //если постзапрос, получаем данные формы
                    {
                        foreach (string str in postData.Split('&'))
                        {
                            var dictStr = str.Split('=');
                            pd[dictStr[0]] = dictStr[1];
                            Console.WriteLine($"{dictStr[0]} {dictStr[1]}");
                        }
                        cook = new Cookie("FirstName", pd["firstname"]);
                        response.AppendCookie(cook);
                        responseStr = $"<html><head></head><body><form method=\"post\">First name: <input type=\"text\" name=\"firstname\" value=\"{pd["firstname"]}\" /><br />Last name: <input type=\"text\" name=\"lastname\"  value=\"{pd["lastname"]}\"   /><input type=\"submit\" value=\"Submit\" /></form></html>";
                    }
                    else         // если гет-запрос отправляем форму
                        responseStr = "<html><head></head><body><form method=\"post\">First name: <input type=\"text\" name=\"firstname\" /><br />Last name: <input type=\"text\" name=\"lastname\" /><input type=\"submit\" value=\"Submit\" /></form></html>";
                }

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseStr);
                // получаем поток ответа и пишем в него ответ
                response.ContentLength64 = buffer.Length;
                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // закрываем поток
                output.Close();
                //Console.WriteLine("k-тое подключение");

            }
            // останавливаем прослушивание подключений
            listener.Stop();
            Console.WriteLine("Обработка подключений завершена");
            Console.Read();
        }
    }
}
