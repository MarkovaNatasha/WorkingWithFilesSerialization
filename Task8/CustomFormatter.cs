using System;

namespace Task8
{
    public class CustomFormatter:IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
            {
                return this;
            }
            return null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (!Equals(formatProvider))
            {
                return null;
            }

            if (String.IsNullOrEmpty(format))
            {
                format = "G";
            }
            string customerString = arg.ToString();
            format = format.ToUpper();
            switch (format)
            {
                case "G":
                    return customerString.Substring(0, 4) + "-"+
                    customerString.Substring(4, 4) + "-" +
                    customerString.Substring(8, 4) + "-" +
                    customerString.Substring(12);
                case "S":
                    return customerString.Substring(0, 4) + " " +
                    customerString.Substring(4, 4) + " " +
                    customerString.Substring(8, 4) + " " +
                    customerString.Substring(12);
                default:
                    throw new FormatException(String.Format("The '{0}' format specifier is not supported.", format));
            }
        }
    }
}
