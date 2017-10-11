using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCodeFirst.Models;
using System.Net;
using System.IO;

namespace EFCodeFirst
{
    class Program
    {
        //static private GruberDB db = new GruberDB();   

        static void Main(string[] args)
        {
            //Salesperson sp = new Salesperson() { Snum = 101, Sname = "Peel", City = "London", Comm = 0.12m };
            //Customer ct = new Customer() { Cnum = 201, Cname = "Hoffman", City = "London", Rating = 100, Salesperson=sp };
            //Order ord = new Order() { Onum = 301, Odate = Convert.ToDateTime("01.01.2017"), Amt = 1005m, Customer = ct, Salesperson = sp };    
            //db.Salespeople.Add(sp);
            //db.Customers.Add(ct);
            //db.Orders.Add(ord);
            //db.SaveChanges();


            HttpListener listener = new HttpListener();
            // установка адресов прослушки
            listener.Prefixes.Add("http://localhost:8888/connection/");;
            listener.Start();
            Console.WriteLine("Ожидание подключений...");
            // метод GetContext блокирует текущий поток, ожидая получение запроса 

            while (true)
            {
                HttpListenerContext context = listener.GetContext();

                HttpListenerRequest request = context.Request;

                HttpListenerResponse response = context.Response;
                StringBuilder sb = new StringBuilder();
                
                sb.Append($"<html><head></head><body><table border='1'><tr><td>Код</td><td>Товар</td><td>Цена</td><td>Количество</td></tr>");

                using (ModelDB db = new ModelDB())
                {
                    foreach (Product p in db.Products)
                        sb.Append($"<tr><td>{p.id}</td><td>{p.name}</td><td>{p.price}</td><td>{p.kolvo}</td></tr>");
                }
                sb.Append($"</table></body></html>");
                
                byte[] buffer = System.Text.Encoding.Default.GetBytes(sb.ToString());
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
