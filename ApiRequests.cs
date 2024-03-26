using BalanceApp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Protocol;
using RestSharp;
using System;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Text;
using static System.Net.Mime.MediaTypeNames;


namespace BalanceApp
{
    static public class ApiRequests
    {

        static public UserData GetUserData(string userId)
        {
            var client = new RestClient("https://localhost:7033/api/App");
            
            var request = new RestRequest();

            request.AddQueryParameter("userId", userId);

            client.Post(request);

            var response = client.Get(request);

            UserData userData = JsonConvert.DeserializeObject<UserData>(response.Content);

            return userData;
        }
        
        
        /// <summary>
        /// User ID
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string getUserId(this ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
                return null;

            ClaimsPrincipal currentUser = user;
            return currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }

    internal class UserTest
    {
        public string userId { get; set; }
    }
}
