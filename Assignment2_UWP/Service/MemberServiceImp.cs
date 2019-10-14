using Assignment2_UWP.Constant;
using Assignment2_UWP.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2_UWP.Service
{
    class MemberServiceImp : MemberService
    {


        public MemberRegister GetInformation(string token)

        {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            Windows.Storage.StorageFile sampleFile = storageFolder.CreateFileAsync("sample.txt",

            Windows.Storage.CreationCollisionOption.ReplaceExisting).GetAwaiter().GetResult();

            Windows.Storage.FileIO.ReadTextAsync(sampleFile).GetAwaiter().GetResult();
            Debug.WriteLine(token);

            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", "Basic " + token);

            var responseContent = client.GetAsync(ApiUrl.GET_INFORMATION_URL).Result.Content.ReadAsStringAsync().Result;

            Debug.WriteLine("Response: " + responseContent);

            var resMember = JsonConvert.DeserializeObject<MemberRegister>(responseContent);


            return resMember;


        }



        public string Login(string username, string password)

        {

            try

            {

                //tạo đối tượng member login từ giá trị của form.

                var memberLogin = new MemberLogin()

                {

                    email = username,

                    password = password

                };

                // validate

                if (!ValidaTeMemberLogin(memberLogin))

                {

                    throw new Exception("Login fails!");

                }

                // lấy token từ api.

                var token = GetTokenFromApi(memberLogin);
                if (token != null)
                {
                    SaveTokenToLocalStorage(token);



                }
                return token;

                //lưu token ra file để dùng lại

            }

            catch (Exception e)

            {
                Debug.WriteLine(e.Message);

                return null;
            }

        }


        public MemberRegister Register(MemberRegister member)

        {

            try

            {


                if (!ValidaTeMemberRegister(member))

                {

                    throw new Exception("Register fails!");

                }

                var httpClient = new HttpClient();

                HttpContent content = new StringContent(JsonConvert.SerializeObject(member), Encoding.UTF8,

                    "application/json");

                var responseContent = httpClient.PostAsync(ApiUrl.API_BASE_URL, content).Result.Content.ReadAsStringAsync().Result;

                // parse member object

                var resMember = JsonConvert.DeserializeObject<MemberRegister>(responseContent);


                return resMember;




            }

            catch (Exception e)

            {

                Debug.WriteLine(e.Message);

                return null;

            }

        }

        private bool ValidaTeMemberRegister(MemberRegister member)
        {

            return true;
        }

        private String GetTokenFromApi(MemberLogin memberLogin)

        {

            // thực hiện request lên api lấy token về.

            var dataContent = new StringContent(JsonConvert.SerializeObject(memberLogin),

                Encoding.UTF8, "application/json");

            var client = new HttpClient();

            var responseContent = client.PostAsync(ApiUrl.LOGIN_URL, dataContent).Result.Content.ReadAsStringAsync().Result;

            var jsonJObject = JObject.Parse(responseContent);

            Debug.WriteLine("Response: " + responseContent);

            MemberLogin resMember = JsonConvert.DeserializeObject<MemberLogin>(responseContent);

            Debug.WriteLine(resMember.email);


            if (jsonJObject["token"] == null)

            {

                throw new Exception("Login fails");

            }

            return jsonJObject["token"].ToString();


        }



        private void SaveTokenToLocalStorage(string token)

        {

            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            Windows.Storage.StorageFile sampleFile = storageFolder.CreateFileAsync("sample.txt",

                Windows.Storage.CreationCollisionOption.ReplaceExisting).GetAwaiter().GetResult();

            Windows.Storage.FileIO.WriteTextAsync(sampleFile, token).GetAwaiter().GetResult();



        }

        private bool ValidaTeMemberLogin(MemberLogin memberLogin)

        {

            return true;

        }
    }
}
