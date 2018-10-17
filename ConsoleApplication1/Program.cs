using Echevil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;



namespace ConsoleApplication1
{
    class Program
    {
        static readonly string[] SizeSuffixes =
                  { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        static string SizeSuffix(Int64 value) /// Gelen giden bağlantı kullanılan miktarlarını mb kb gb cinsine çeviriyor
        {
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return "0.0 bytes"; }

            int mag = (int)Math.Log(value, 1024);
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            return string.Format("{0:n1} {1}", adjustedSize, SizeSuffixes[mag]);
        }

        
        static private void CheckSpeed()  //// Bağlantı Hızı Konrtol Ediliyor kb mb
        {
            double[] speeds = new double[2];
            for (int i = 0; i < 1; i++)
            {
                int jQueryFileSize = 261; //Size of File in KB.
                WebClient client = new WebClient();
                DateTime startTime = DateTime.Now;
                client.DownloadFile("http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.js", Path.GetDirectoryName("~/Dosya"));
                DateTime endTime = DateTime.Now;
                speeds[i] = Math.Round((jQueryFileSize / (endTime - startTime).TotalSeconds));
            }

            Console.WriteLine("Download Speed: {0}KB/s", speeds.Average());

        }


        static void Main(string[] args)
        {

            if (!NetworkInterface.GetIsNetworkAvailable())
                return;

            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces(); /// Kullanılan bağlantılar....

            foreach (NetworkInterface ni in interfaces)
            {

                if (ni.Name == "Wi-Fi" || ni.Name== "Kablosuz Ağ Bağlantısı") /// Wifi ise bloka gir
                {
                    Console.WriteLine(ni.Name);
                    Console.WriteLine("Bytes Sent: {0}", SizeSuffix(ni.GetIPv4Statistics().BytesSent));
                    Console.WriteLine("Bytes Received: {0}", SizeSuffix(ni.GetIPv4Statistics().BytesReceived));
                    Console.WriteLine("\n");                  

                }
                //else if (ni.Name == "Ethernet 2") ////Diğer Seçenek ..sizde farklı olabilir.
                //{

                //    Console.WriteLine(ni.Name);
                //    Console.WriteLine("Bytes Sent: {0}", SizeSuffix(ni.GetIPv4Statistics().BytesSent));
                //    Console.WriteLine("Bytes Received: {0}", SizeSuffix(ni.GetIPv4Statistics().BytesReceived));


                //}
                //else if (ni.Name == "Ethernet") // Bu kısım bağlantı gittiğinde sıfırlanmalı!!
                //{
                //    Console.WriteLine(ni.Name);
                //    Console.WriteLine("Bytes Sent: {0}", SizeSuffix(ni.GetIPv4Statistics().BytesSent));
                //    Console.WriteLine("Bytes Received: {0}", SizeSuffix(ni.GetIPv4Statistics().BytesReceived));
                //    Console.WriteLine("\n");

                //    IPv4InterfaceStatistics interfaceStats = ni.GetIPv4Statistics();
                   

                //    string ifade = Console.ReadLine();
                //    string gelen = SizeSuffix(interfaceStats.BytesReceived);                
                  
                   
               


                //    CheckSpeed();
                //}

            }



            Console.ReadLine();

        }
    }
}
