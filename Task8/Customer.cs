using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Runtime.Serialization;

namespace Task8
{
    [Serializable]
    public class Customer :ISerializable
    {
        private string name;
        private string surname;
        private DateTime dayBirth;
        private string phone;
        private string email;
        private string passport;
        private long cardNumber;
        private DateTime cardDate;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }
        public DateTime DayBirth
        {
            get { return dayBirth; }
            set { dayBirth = value; }
        }
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Passport
        {
            get { return passport; }
            set { passport = value; }
        }
        public long CardNumber
        {
            get { return cardNumber; }
            set { cardNumber = value; }
        }
        public DateTime CardDate
        {
            get { return cardDate; }
            set { cardDate = value; }
        }

        public Customer()
        {
        }

        public Customer(string name, string surname, DateTime dayBirth, string phone, string email, string passport, long cardNumber, DateTime cardDate)
        {
            Name = name;
            Surname = surname;
            DayBirth = dayBirth;
            Phone = phone;
            Email = email;
            Passport = passport;
            CardNumber = cardNumber;
            CardDate = cardDate;
        }

        protected Customer(SerializationInfo info, StreamingContext context)
        {
            if(info == null)
            {
                throw new ArgumentException("info");
            }
            name = (string) info.GetValue("nameCustomer", typeof (string));
            surname = (string)info.GetValue("surnameCustomer", typeof(string));
            dayBirth = (DateTime)info.GetValue("dayBirthCustomer", typeof(DateTime));
            phone = (string)info.GetValue("phoneCustomer", typeof(string));
            email = (string)info.GetValue("emailCustomer", typeof(string));
            passport = (string)info.GetValue("passportCustomer", typeof(string));
            cardNumber = (long)info.GetValue("cardNumberCustomer", typeof(long));
            cardDate = (DateTime)info.GetValue("cardDateCustomer", typeof(DateTime));
        }

        public string FoundSurname(string strPattern)
        {
            if (Regex.IsMatch(Surname, strPattern, RegexOptions.IgnoreCase))
            {
                return string.Format("This surname is matching with pattern -> {0}",Surname);
            }
            return "No matches...";
        }

        public string FoundName(string strPattern)
        {
            if (Regex.IsMatch(Name, strPattern, RegexOptions.IgnoreCase))
            {
                return string.Format("This name is matching with pattern -> {0}", Name);
            }
            return "No matches...";
        }

        public void Print()
        {
            Console.WriteLine("{0,7} | {1,10} | {2, 10} | {3,20} | {4,20} | {5,10} | " + 
                String.Format(new CustomFormatter(), "{0}", CardNumber) + " | {6,16}",
                Name, Surname, DayBirth.ToString("d", CultureInfo.CurrentCulture), Phone, 
                Email, Passport, CardDate.ToString("g", CultureInfo.CurrentCulture));           
        }

        public class SortBySurname : IComparer<Customer>
        {
            public int Compare(Customer x, Customer y)
            {
                return String.Compare(x.Surname, y.Surname);
            }
        }

        public class SortByDateBirth : IComparer<Customer>
        {
            public int Compare(Customer x, Customer y)
            {
                return DateTime.Compare(x.DayBirth, y.DayBirth);
            }
        }

        public class SortByPhone : IComparer<Customer>
        {
            public int Compare(Customer x, Customer y)
            {
                return String.Compare(x.Phone, y.Phone);
            }
        }

        public void ChangeIncorrectData()
        {
            if(!char.IsUpper(Name[0]))
            {
                Name = Name.Remove(1, Name.Length - 1).ToUpper() + Name.Remove(0,1);
            }
            if (!char.IsUpper(Surname[0]))
            {
                Surname = Surname.Remove(1, Surname.Length - 1).ToUpper() + Surname.Remove(0, 1);
            }
            if(Phone.Length > 17)
            {
                Phone = Phone.Remove(17);
            }
            if (Phone.Length < 17)
            {
                int dx = 17 - Phone.Length;
                var addStr = new string('x', dx);
                Phone = Phone.Replace(Phone, Phone + addStr);
            }
            if(Passport.Length > 9)
            {
                Passport = Passport.Remove(9);
            }
            if(Passport.Length < 9)
            {
                int dx = 9 - Passport.Length;
                var addStr = new string('0', dx);
                Passport = Passport.Insert(Passport.Length, addStr);
            }
        }

        public static bool CheckNameSurname(string name)
        {
            return Regex.IsMatch(name, @"^[a-zA-Z']{1,20}$");
        }

        public static bool CheckDate(string date)
        {
            return Regex.IsMatch(date, @"(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d");
        }

        public static bool CheckPhone(string phone)
        {
            return Regex.IsMatch(phone, @"(\+380)\s(\d{2})\s(\d{3})\s(\d{2})\s(\d{2})");
        }

        public static bool CheckEmail(string email)
        {
            return Regex.IsMatch(email, @"([A-Z;a-z;0-9;\x2E;\x2D;_]+)?@([A-Z;a-z;0-9;\x2E;-;]+)?");
        }

        public static bool CheckPassport(string passport)
        {
            return Regex.IsMatch(passport, @"[A-Z]{2}\s(\d{6})");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }
            info.AddValue("nameCustomer", name, typeof(string));
            info.AddValue("surnameCustomer", surname, typeof(string));
            info.AddValue("dayBirthCustomer", dayBirth, typeof(DateTime));
            info.AddValue("phoneCustomer", phone, typeof(string));
            info.AddValue("emailCustomer", email, typeof(string));
            info.AddValue("passportCustomer", passport, typeof(string));
            info.AddValue("cardNumberCustomer", cardNumber, typeof(long));
            info.AddValue("cardDateCustomer", cardDate, typeof(DateTime));
        }
    }
}
