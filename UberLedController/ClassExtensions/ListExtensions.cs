using System.Collections.Generic;
using System.Text;

namespace UberLedController.ClassExtensions
{
    public static class ListExtensions
    {
        public static string ToFullString<T>(this IList<T> inp)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append('{');
            sb.Append(inp[0]);
            for (int i = 1; i < inp.Count; i++)
            {
                string item = inp[i].ToString();
                sb.Append(", ");
                sb.Append(item);
            }
            sb.Append('}');
            return sb.ToString();
        }
    }
}
