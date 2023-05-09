﻿using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace TheGame.Multiplayer
{
    public static class MultiplayerCommunicationService
    {
        public static async void GetDataFromApi()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("192.168.1.101:580");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("/multiplayer/refresh");

            }
        }

        public static async Task<string> AuthenticateUser(string username, string password)
        {
            
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("http://thegame2-backend-env.eba-4tmmzhaz.eu-central-1.elasticbeanstalk.com");
                client.BaseAddress = new Uri("https://localhost:44348");
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
                //client.BaseAddress = new Uri("http://thegame2-backend-env.eba-4tmmzhaz.eu-central-1.elasticbeanstalk.com");
                client.BaseAddress = new Uri("https://localhost:44348");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("username", username);
                client.DefaultRequestHeaders.Add("token", token);
                HttpResponseMessage res = await client.GetAsync("/user/VerifyGameInstance");
                if (!res.IsSuccessStatusCode)
                    throw new Exception();


            }
        }
    }
}
