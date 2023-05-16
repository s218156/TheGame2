using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TheGame.Multiplayer.Models;
#if ANDROID
using static Android.Media.Session.MediaSession;
#endif

namespace TheGame.Multiplayer
{
    public static class MultiplayerCommunicationService
    {
        private static string apiURL ="http://thegame2-backend-env.eba-4tmmzhaz.eu-central-1.elasticbeanstalk.com";
        //private static string apiURL = "https://localhost:44348";

        public static async Task<string> AuthenticateUser(string username, string password)
        {
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("username", username);
                client.DefaultRequestHeaders.Add("password", CustomEncryption.EncryptPasswordForHeader(username, password));
                HttpResponseMessage res = await client.GetAsync("/user/AuthGameInstance");
                if (res.IsSuccessStatusCode)
                    return res.Content.ReadAsStringAsync().Result;
                else
                    return "";


            }
        }

        public static async Task VerifyUserConfig(string username, string token)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("username", username);
                client.DefaultRequestHeaders.Add("token", token);
                HttpResponseMessage res = await client.GetAsync("/user/VerifyGameInstance");
                if (!res.IsSuccessStatusCode)
                    throw new Exception();


            }
        }

        public static async Task<MultiplayerObject> GetMultiplayerObject(MultiplayerData data)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("username", data.username);
                client.DefaultRequestHeaders.Add("token", data.userPrivateKey);
                HttpResponseMessage res = await client.GetAsync("/user/GetDataOfUser");
                if (!res.IsSuccessStatusCode)
                    throw new Exception();
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<MultiplayerObject>(res.Content.ReadAsStringAsync().Result);
            }
        } 

        public static async Task<bool> JoinPlayer(PlayerModel model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var postTask = client.PostAsJsonAsync<PlayerModel>("/multiplayer/JoinSession", model);
                postTask.Wait();
                var result = postTask.Result;
                return result.IsSuccessStatusCode;
            }
        }

        public static async Task SendPlayerData(PlayerModel model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var postTask = client.PostAsJsonAsync<PlayerModel>("/multiplayer/UpdatePlayerData", model);
                postTask.Wait();
                var result = postTask.Result;
            }
        }
        public static async Task<bool> CheckApiAvalability()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync("/api/CheckApiAvalability");
                if (!res.IsSuccessStatusCode)
                    throw new Exception();
                else
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(res.Content.ReadAsStringAsync().Result);
            }
        }

        public static async Task<List<PlayerModel>> RefreshSessionData(PlayerModel model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiURL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var postTask = client.PostAsJsonAsync<PlayerModel>("/multiplayer/refreshSessionData", model);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<List<PlayerModel>>(result.Content.ReadAsStringAsync().Result);
                }
                else
                        return new List<PlayerModel>(); 
            }
        }
    }
}
