using BalanceApp.Models;
using Humanizer;
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
        static private RestClient client = new RestClient("https://localhost:7033/api/App");

        static public async Task<UserData> GetUserDataAsync(string userId)
        {
            var request = new RestRequest();

            request.AddQueryParameter("userId", userId);

            await client.PostAsync(request);

            var response = client.Get(request);

            UserData userData = JsonConvert.DeserializeObject<UserData>(response.Content);

            return userData;
        }

        static public async Task<UserTask> GetTaskAsync(int id, string userId) {
            var request = new RestRequest("get/" + id);

            request.AddQueryParameter("userId", userId);

            var response = await client.GetAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<UserTask>(response.Content);
            }

            return null;
        }

        static public async Task<bool> CreateTask(string userId, UserTask userTask)
        {
            var request = new RestRequest("create");

            request.AddQueryParameter("userId", userId);

            request.AddJsonBody(userTask);

            var response = await client.PostAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        static public async Task<bool> EditTask(int id, string userId, UserTask userTask)
        {
            var request = new RestRequest("edit/" + id);

            request.AddQueryParameter("userId", userId);

            request.AddJsonBody(userTask);

            var response = await client.PutAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        static public async Task<bool> DeleteTaskAsync(int id, string userId)
        {
            var request = new RestRequest("delete/" + id);

            request.AddQueryParameter("userId", userId);

            var response = await client.DeleteAsync(request);

            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
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
}
