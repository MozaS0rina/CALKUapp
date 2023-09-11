using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CALKU.Models;

namespace CALKU.Services
{
    public class RegisterService : IRegisterRepository
    {
        public async Task<UserInfo> Register(string userName, string password, string email)
        {
            HttpClientHandler handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            using (HttpClient client = new HttpClient(handler))
            {
                var userInfo = new UserInfo();
                //  var client = new HttpClient();

                //https://localhost:44346/api/Registration/LoginUserAsync/

                string url = "https://10.0.2.2:7213/api/Registration/RegisterUserAsync/"+userName+"/"+password+"/"+email;

                client.BaseAddress = new Uri(url);

                var jsonContent= new StringContent(JsonConvert.SerializeObject(userInfo), Encoding.UTF8, "application/json");//content for post method in api
                HttpResponseMessage response = await client.PostAsync(client.BaseAddress,jsonContent);
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
