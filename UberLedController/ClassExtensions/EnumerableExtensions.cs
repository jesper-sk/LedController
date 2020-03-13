using System.Collections.Generic;
using System.Linq;
using System.Text;
using UberLedController.Graphics;

namespace UberLedController.ClassExtensions
{
    public static class EnumerableExtensions
    {
        public static string ToFullString<T>(this IEnumerable<T> inp)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('{');
            foreach (T item in inp)
            {
                string str = item.ToString();
                sb.Append(str);
                sb.Append(", ");
            }
            sb.Remove(sb.Length - 2, 2);
            sb.Append('}');
            return sb.ToString();
        }
    }
}
