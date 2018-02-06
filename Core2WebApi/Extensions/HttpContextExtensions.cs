using Core2WebApi.Entities.Session;
using Microsoft.AspNetCore.Http;

namespace Core2WebApi.Extensions
{
    public static class HttpContextExtensions
    {
        public static SessionUserModel GetMiyaUser(this HttpContext context)
        {
            if(context.Items.ContainsKey("MiyaUser"))
                    return (SessionUserModel)context.Items["MiyaUser"];
            return new SessionUserModel();
        }

        public static string GetPublicKey(this HttpContext context)
        {
            if(context.Items.ContainsKey("PublicKey"))
                    return context.Items["PublicKey"].ToString();
            return "";
        }

        public static string GetUserName(this HttpContext context)
        {
            if(context.Items.ContainsKey("MiyaUserName"))
                    return context.Items["MiyaUserName"].ToString();
            return "";
        }

        public static string GetUserPassword(this HttpContext context)
        {
            if(context.Items.ContainsKey("MiyaUserPassword"))
                            return context.Items["MiyaUserPassword"].ToString();
            return "";
        }

        public static string GetPrivateKey(this HttpContext context)
        {
            if(context.Items.ContainsKey("PrivateKey"))
                            return context.Items["PrivateKey"].ToString();
            return "";
        }

        public static string GetToken(this HttpContext context)
        {
            return context.Items["PrivateKey"].ToString();
        }
    }
}
