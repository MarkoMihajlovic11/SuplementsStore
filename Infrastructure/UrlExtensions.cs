using Microsoft.AspNetCore.Http;

namespace SuplementsStore1.Infrastructure
{
    //metoda kreira URL na koji će korisnik biti vraćen nakon ažuriranja korpe
    public static class UrlExstensions
    {
        public static string PathAndQuery(this HttpRequest request) =>
            request.QueryString.HasValue ?
            $"{request.Path}{request.QueryString}" :
            request.Path.ToString();
    }
}