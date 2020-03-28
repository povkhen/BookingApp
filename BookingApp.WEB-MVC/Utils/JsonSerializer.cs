using Newtonsoft.Json;

namespace BookingApp.WEB_MVC.Utils
{
    public static class JsonSerializer
    {
        public static string ToJSON(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}