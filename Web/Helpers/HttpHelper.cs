using System.Web;

namespace Web.Helpers;
public static class HttpHelper
{
    public static string GetQueryString(object obj)
    {
        if (obj is null) return null;

        var properties = from p in obj.GetType().GetProperties()
                         where p.GetValue(obj, null) != null
                         select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

        return string.Join("&", properties.ToArray());
    }
}
