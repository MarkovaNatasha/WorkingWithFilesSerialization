using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Task8
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Серіалізація одного екземляру клієнта
            const string filePath = "customer.dat";
            const string filePathXML = "customer.xml";

            var customer = new Customer("Ivan","Ivanov",new DateTime(1990, 12, 11),"+380 98 765 56 45",
                "ivanov@gmail.com", "LL 678567", 1234567876543234, new DateTime(2013, 11, 11));
            customer.Print();
            MySerialization.SaveBinary(filePath, customer);
            MySerialization.LoadBinary(filePath).Print();
            MySerialization.SaveXML(filePathXML, customer);
            MySerialization.LoadXML(filePathXML).Print();

            //Серіалізація списку клієнтів
            var customers = new List<Customer>
                                           {
                                               new Customer("olga", "popova", new DateTime(1991, 11, 24), "+380 98 345 35 64 78",
                                                            "popova@gmail.com", "AT 342546", 2345353546345634,
                                                            new DateTime(2011, 06, 23, 11,23,45)),
                                               new Customer("ivan", "Stepanov", new DateTime(1989, 01, 23), "+380 98 456 78 347",
                                                            "i.stepanov@i.ua", "TT 3445563", 4567345624576535,
                                                            new DateTime(2000, 01, 12, 14,11,7)),
                                               new Customer("Pavlo", "Kotov", new DateTime(1978, 04, 13), "+380 88 342 57",
                                                            "OKotov1201@info.com", "GH 4567876", 8976354687563546,
                                                            new DateTime(2012, 07, 11, 10,10,34)),
                                               new Customer("Maria", "leonova", new DateTime(1990, 08, 07), "+380 50 345 26 78",
                                                            "m.leon1990@gmail.com", "HJ 4567", 7675345634564656,
                                                            new DateTime(2013, 03, 23, 11, 34, 44)),
                                               new Customer("Stepan", "Luk", new DateTime(1988, 03, 05), "+380 66 343 54 56",
                                                            "jjuukk@mail.ru", "JK 4567765", 4563564575673456,
                                                            new DateTime(2011, 06, 11,12,13,13)),
                                           };
            MySerialization.SaveListXML(filePathXML, customers);
            List<Customer> descustomers = MySerialization.LoadListXML(filePathXML);
            Console.WriteLine("Deserialization list of customers:");
            foreach (var descustomer in descustomers)
            {
                descustomer.Print();
            }

            //Серіалязація за допомогою інтерфейсу ISerializable
            Console.WriteLine("Serialization with ISerialization:");
            const string fileName = "customers.myData";
            IFormatter formatter = new BinaryFormatter();
            SerializeItem(fileName, formatter);
            DeserializeItem(fileName, formatter).Print();
            Console.WriteLine();
            Console.WriteLine();

            //Пошук текстового файлу в заданій директорії по зададому шаблону
            var openFile = new FolderBrowserDialog();
            openFile.ShowDialog();
            var foundFile = new FoundFile(@"^a[a-z]*", openFile.SelectedPath);
            Console.WriteLine(foundFile.GetFiles());
            
            Console.ReadKey();
        }

        public static void SerializeItem(string fileName, IFormatter formatter)
        {
            var customer = new Customer
                                    {
                                        Name = "Petro",
                                        Surname = "Petrov",
                                        DayBirth = new DateTime(1990, 8, 12),
                                        Phone = "+380 56 456 45 45",
                                        Email = "petrov@df.kl",
                                        Passport = "DD 234567",
                                        CardNumber = 1234567812345678,
                                        CardDate = new DateTime(2012, 11, 12)
                                    };

            using(var fs = new FileStream(fileName, FileMode.Create))
            {
                formatter.Serialize(fs, customer);
            }
         }


        public static Customer DeserializeItem(string fileName, IFormatter formatter)
        {
            using (var fs = new FileStream(fileName, FileMode.Open))
            {
                return (Customer)formatter.Deserialize(fs);
            }
        }       

    }
}
