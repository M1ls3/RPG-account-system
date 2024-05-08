using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace RPG_account_system
{
    public static class EnumHelper
    {
        public static string GetDescription(Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            return value.ToString();
        }

        public static Class GetClassFromDescription(string description)
        {
            foreach (Class char_class in Enum.GetValues(typeof(Class)))
            {
                string typeDescription = GetDescription(char_class);
                if (typeDescription == description)
                {
                    return char_class;
                }
            }
            throw new ArgumentException("No matching enum value found for the given description.");
        }

        public static Category GetCategoryFromDescription(string description)
        {
            foreach (Category char_class in Enum.GetValues(typeof(Category)))
            {
                string typeDescription = GetDescription(char_class);
                if (typeDescription == description)
                {
                    return char_class;
                }
            }
            throw new ArgumentException("No matching enum value found for the given description.");
        }
    }

    public class Request
    {
        public static NpgsqlConnection GetConnection()
        {
            return new NpgsqlConnection(@"Host=localhost;Username=postgres;Password=paloma123;Database=postgres;Encoding=UTF8");
        }
    }
}
