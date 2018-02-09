using Core2WebApi.Entities.Session;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Core2WebApi.Extensions
{
    public static class DistributedCacheExtensions
    {
        public static bool PublicKeyExists(this IDistributedCache cache, string publicKey)
        {
            var cacheUser = cache.GetString(publicKey);
            var user = JsonConvert.DeserializeObject<SessionUserModel>(cacheUser);
            if(user != null)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
