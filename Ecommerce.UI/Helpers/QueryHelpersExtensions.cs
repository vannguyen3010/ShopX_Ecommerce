using Microsoft.Extensions.Primitives;
using System.Net;

namespace Ecommerce.UI.Helpers
{
    public static class QueryHelpersExtensions
    {
        public static string ToQueryString(this IDictionary<string, StringValues> query)
        {
            return string.Join("&", query.Select(kvp => $"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value.ToString())}"));
        }
    }
}
