using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;

namespace Task8
{
    public static class MySerialization
    {
        // Методи для бінарної і xml серіалізації одного екземпляру
        public static void SaveBinary(string fileName, Customer customer)
        {
            using (var fs = File.Create(fileName))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(fs, customer);
            }
        }

        public static Customer LoadBinary(string fileName)
        {
            using (var fs = File.Open(fileName, FileMode.Open))
            {
                var bf = new BinaryFormatter();
                return (Customer)bf.Deserialize(fs);
            }
        }

        public static void SaveXML(string fileName, Customer customer)
        {
            using (var sw = new StreamWriter(fileName))
            {
                var xmlSer = new XmlSerializer(customer.GetType());
                xmlSer.Serialize(sw, customer);
            }
        }

        public static Customer LoadXML(string fileName)
        {
            using (var sr = new StreamReader(fileName))
            {
                var xmlDeser = new XmlSerializer(typeof (Customer));
                return (Customer) xmlDeser.Deserialize(sr);
            }
        }

        // Методи для бінарної і xml серіалізації списку клієнтів
        public static void SaveListBinary(string fileName, List<Customer> customer)
        {
            using (var fs = File.Create(fileName))
            {
                var bf = new BinaryFormatter();
                bf.Serialize(fs, customer);
            }
        }

        public static List<Customer> LoadListBinary(string fileName)
        {
            using (var fs = File.Open(fileName, FileMode.Open))
            {
                var bf = new BinaryFormatter();
                return (List<Customer>)bf.Deserialize(fs);
            }
        }

        public static void SaveListXML(string fileName, List<Customer> customer)
        {
            using (var sw = new StreamWriter(fileName))
            {
                var xmlSer = new XmlSerializer(customer.GetType());
                xmlSer.Serialize(sw, customer);
            }
        }

        public static List<Customer> LoadListXML(string fileName)
        {
            using (var sr = new StreamReader(fileName))
            {
                var xmlDeser = new XmlSerializer(typeof(List<Customer>));
                return (List<Customer>)xmlDeser.Deserialize(sr);
            }
        }
    }
}
