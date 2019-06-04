using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;

namespace PolylineApp1
{
    public class Polyline
    {
        public int[] x;
        public int[] y;

        public Polyline()
        {
            int count = 5;
            x = new int[count];
            y = new int[count];
            for (int i = 0; i < count; i++)
            {
                x[i] = i * i;
                y[i] = i;
            }
        }

        public Polyline(int start, int count)
        {
            x = new int[count];
            y = new int[count];
            for (int i = 0; i < count; i++)
            {
                x[i] = (start + i) * (i + start);
                y[i] = i+start;
            }
        }
        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < x.Length; i++)
            {
                s += "x[" + i + "] = " + x[i] + "\ty[" + i + "] = " + y[i] + "\n";
            }
            return s;
        }
    }


    class Program
    {
        public static void SerialiazingJSON(Polyline poly1, string polyWay)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Polyline));

            using (FileStream fs = new FileStream(polyWay, FileMode.OpenOrCreate))
            {
                jsonFormatter.WriteObject(fs, poly1);
            }
        }
        public static Polyline DeserialiazingJSON(string polyWay)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Polyline));

            using (FileStream fs = new FileStream(polyWay, FileMode.OpenOrCreate))
            {
                Polyline newPoly = (Polyline)jsonFormatter.ReadObject(fs);                
                return newPoly;
            }
        }
        public static void SerialiazingXml(Polyline poly1, string polyWay)
        {
            // передаем в конструктор тип класса
            XmlSerializer formatter = new XmlSerializer(typeof(Polyline));

            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream(polyWay, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, poly1);
            }

            
        }
        public static Polyline DeserialiazingXml(string polyWay)
        {
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(Polyline));
                using (FileStream fs = new FileStream(polyWay, FileMode.OpenOrCreate))
                {
                    Polyline newPoly = (Polyline)formatter.Deserialize(fs);
                    return newPoly;
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return new Polyline();
            }
        }
        public static void ShowMenu()
        {
            Console.WriteLine("1. Create new polyline");
            Console.WriteLine("2. Save to XML file");
            Console.WriteLine("3. Save to JSON file");
            Console.WriteLine("4. Load XML");
            Console.WriteLine("5. Load JSON");
            Console.WriteLine("6. Exit");
        }
        static void Main(string[] args)
        {
            bool isExit = true;
            Polyline polyIn = new Polyline();
           // Polyline polyOut = new Polyline();
            while (isExit)
            {
                ShowMenu();
                string s = Console.ReadLine();
                switch (s)
                {
                    case "1":
                        Random r = new Random();
                        polyIn = new Polyline(r.Next(10), r.Next(10));
                        Console.WriteLine(polyIn);
                        break;
                    case "2":
                        SerialiazingXml(polyIn, "poly2.xml");
                        break;
                    case "3":
                        SerialiazingJSON(polyIn, "poly1.json");
                        break;
                    case "4":
                        Console.WriteLine(DeserialiazingXml("poly2.xml"));
                        break;
                    case "5":
                        Console.WriteLine(DeserialiazingJSON("poly1.json"));
                        break;
                    case "6":
                        isExit = false;
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
