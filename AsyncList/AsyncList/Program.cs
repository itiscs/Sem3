using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

using System.Threading.Tasks;

namespace AsyncList
{

    class Program
    {
        private static Dictionary<string, string> sessions = new Dictionary<string, string>();

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

        public static Dictionary<string, string> GetDict(string postData)
        {
            Dictionary < string, string> pd = new Dictionary<string, string>();
            foreach (string str in postData.Split('&'))
            {
                var dictStr = str.Split('=');
                pd[dictStr[0]] = dictStr[1];
            }
            return pd;
        }

        static void StreamFromFile(string path, HttpListenerContext context)
        {
            using (FileStream fs = File.Open(path, FileMode.Open))
            {

                var bytes = new byte[fs.Length];
                fs.Read(bytes, 0, bytes.Length);
                context.Response.ContentType = "text/HTML";
                context.Response.ContentLength64 = bytes.Length;
                context.Response.OutputStream.Write(bytes, 0, bytes.Length);
            }

        }

        static void StreamFromSession(HttpListenerContext context, Cookie cook)
        {
            string responseStr = $"<html><head></head><body>Hello, {sessions[cook.Value]}</body></html>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseStr);
            // получаем поток ответа и пишем в него ответ
            context.Response.ContentLength64 = buffer.Length;
            Stream output = context.Response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // закрываем поток
            output.Close();
        }


        static void Main(string[] args)
        {
             Console.WriteLine("Running async server.");
              new AsyncServer();
        }

        class AsyncServer
        {
            public AsyncServer()
            {
                var listener = new HttpListener();
                //listener.AuthenticationSchemes = AuthenticationSchemes.Basic;

                listener.Prefixes.Add("http://localhost:8081/");
                listener.Prefixes.Add("http://127.0.0.1:8081/");

                listener.Start();

                while (true)
                {
                    try
                    {
                        var context = listener.GetContext();
                        ThreadPool.QueueUserWorkItem(o => HandleRequest(context));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            private void HandleRequest(object state)
            {
                try
                {
                    var context = (HttpListenerContext)state;
                    Cookie cook = context.Request.Cookies["Session"];

                    if (cook == null)
                    {
                        string postData = GetRequestPostData(context.Request);
                        if (postData == null)
                        {
                            StreamFromFile(@"..\..\Views\Page1.html", context);                            
                        }
                        else
                        {
                            var pd = GetDict(postData);
                            if (pd["login"] == "admin" && pd["password"] == "pass")
                            {
                                string guid = Guid.NewGuid().ToString();
                                sessions.Add(guid, pd["login"]);
                                cook = new Cookie("Session", guid);
                                context.Response.AppendCookie(cook);
                                StreamFromSession(context, cook);                    
                            }
                            else
                            {
                                context.Response.Redirect("http://localhost:8081/");
                            }

                        }
                    }
                    else
                    {
                        if (sessions.Keys.Contains(cook.Value))
                            StreamFromSession(context, cook);
                        else
                            context.Response.Redirect("http://localhost:8081/");


                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    // Client disconnected or some other error - ignored for this example
                }
            }
        }
    }
}
