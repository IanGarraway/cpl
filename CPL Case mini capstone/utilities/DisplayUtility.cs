using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPL_Case_mini_capstone.utilities
{
    internal class DisplayUtility
    {
        /// <summary>
        /// Displays a specific property or formatted string representation of each item in a list.
        /// This method is generic and can handle any object type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"> A list of objects containing the data to be displayed</param>
        /// <param name="propertySelector"> Use a lambda expression to specify the data item or items to be displayed</param>
        public static void DisplayListByProperty<T>(List<T> items, Func<T, string> propertySelector)
        {
            foreach (T item in items)
            {
                Console.WriteLine(propertySelector(item));
            }
        }
    }
}
