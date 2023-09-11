using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using CALKU.Models;
using Newtonsoft.Json;

namespace CALKU.Services
{
    public class LoginService : ILoginRepository
    {
        public async Task<UserInfo> Login(string userName, string password)
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            using (HttpClient client = new HttpClient(handler))
            {
                var userInfo = new UserInfo();
                //var client = new HttpClient();
                // client.Timeout = TimeSpan.FromSeconds(200);

                //https://localhost:44346/api/Registration/LoginUserAsync/
                //192.168.1.2
                //10.0.2.2

                string url = "https://10.0.2.2:7213/api/Registration/LoginUserAsync/" + userName + "/" + password;

                client.BaseAddress = new Uri(url);
                HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
                if (response.IsSuccessStatusCode)
                {
                    string content = response.Content.ReadAsStringAsync().Result;
                    userInfo = JsonConvert.DeserializeObject<UserInfo>(content);
                    return await Task.FromResult(userInfo);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
