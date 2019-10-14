using Assignment2_UWP.Constant;
using Assignment2_UWP.Entity;
using Assignment2_UWP.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using Windows.Foundation;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Assignment2_UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Register : Page
    {
        private MemberServiceImp memberService;
        int gender;
        private StorageFile photo;

        private object contentType;
        private string ImageUrl;

        public Register()

        {

            this.InitializeComponent();
            this.memberService = new MemberServiceImp();
        }



        private async void Button_Register_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (this.Password.Password != this.Confirm.Password)
            {
                return;
            }

            var member = new MemberRegister

            {

                firstName = this.FirstName.Text,

                lastName = this.LastName.Text,

                password = this.Password.Password,

                address = this.Address.Text,

                avatar = this.Avatar.Text,

                birthday = this.Birthday.Date.ToString("yyyy-MM-dd"),

                email = this.Email.Text,

                gender = this.gender,

                introduction = this.Introduction.Text,

                phone = this.Phone.Text

            };

            // validate phía client.


            Dictionary<string, string> errors = Validate.ValidateMember(member);
            if (errors.Count < 0)
            {
                if (errors.ContainsKey("FirstName"))
                {
                    FirstName.Text = errors["FirstName"];
                    FirstName.Visibility = Visibility.Visible;
                }
                if (errors.ContainsKey("LastName"))
                {
                    LastName.Text = errors["LastName"];
                    LastName.Visibility = Visibility.Visible;
                }
                if (errors.ContainsKey("Password"))
                {
                    Password.Password = errors["Password"];
                    Password.Visibility = Visibility.Visible;
                }
                if (errors.ContainsKey(""))
                {
                    Confirm.Password = errors["Confirm Password"];
                    Confirm.Visibility = Visibility.Visible;
                }
                if (errors.ContainsKey("Address"))
                {
                    Address.Text = errors["Address"];
                    Address.Visibility = Visibility.Visible;
                }
                if (errors.ContainsKey("Avatar"))
                {
                    Avatar.Text = errors["Avatar"];
                    Avatar.Visibility = Visibility.Visible;
                }
                if (errors.ContainsKey("Birthday"))
                {
                    Birthday.DataContext = errors["Birthday"];
                    Birthday.Visibility = Visibility.Visible;
                }
                if (errors.ContainsKey("Email"))
                {
                    Email.Text = errors["Email"];
                    Email.Visibility = Visibility.Visible;
                }
                if (errors.ContainsKey("Gender"))
                {
                    Gender.Text = errors["Gender"];
                    Gender.Visibility = Visibility.Visible;
                }
                if (errors.ContainsKey("Introduction"))
                {
                    Introduction.Text = errors["Introdution"];
                    Introduction.Visibility = Visibility.Visible;
                }
                if (errors.ContainsKey("Phone"))
                {
                    Phone.Text = errors["Phone"];
                    Phone.Visibility = Visibility.Visible;
                }
                return;


            }

            Debug.WriteLine(JsonConvert.SerializeObject(member));

            var httpClient = new HttpClient();
            HttpContent content = new StringContent(JsonConvert.SerializeObject(member), Encoding.UTF8, "application/json");
            string responseContent = httpClient.PostAsync(ApiUrl.API_BASE_URL, content).Result.Content.ReadAsStringAsync().Result;

            Debug.WriteLine("Response: " + responseContent);

            MemberRegister resMember = JsonConvert.DeserializeObject<MemberRegister>(responseContent);

            Debug.WriteLine(resMember.email);


            if (member != null)

            {

                var message = new MessageDialog("Register failed !");
                await message.ShowAsync();

            }

            else

            {
                var message = new MessageDialog("Register success");
                await message.ShowAsync();

            }

        }

        private void RadioButton_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            if (radioButton != null)
            {
                gender = int.Parse(radioButton.Tag.ToString());
            }


        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();

            var uploadUrl = client.GetAsync("https://2-dot-backup-server-003.appspot.com/get-upload-token").Result.Content.ReadAsStringAsync().Result;

            Debug.WriteLine("Upload url: " + uploadUrl);



            CameraCaptureUI captureUI = new CameraCaptureUI();

            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;

            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);



            this.photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);



            if (this.photo == null)

            {

                // User cancelled photo capture

                return;

            }

            HttpUploadFile(uploadUrl, "myFile", "image/png");


        }

        private async void HttpUploadFile(string url, string paramName, string contentType)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");

            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");



            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);

            wr.ContentType = "multipart/form-data; boundary=" + boundary;

            wr.Method = "POST";



            Stream rs = await wr.GetRequestStreamAsync();

            rs.Write(boundarybytes, 0, boundarybytes.Length);



            string header = string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n", paramName, "path_file", contentType);

            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);

            rs.Write(headerbytes, 0, headerbytes.Length);



            // write file.

            Stream fileStream = await this.photo.OpenStreamForReadAsync();

            byte[] buffer = new byte[4096];

            int bytesRead = 0;

            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)

            {

                rs.Write(buffer, 0, bytesRead);

            }


            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");

            rs.Write(trailer, 0, trailer.Length);



            WebResponse wresp = null;

            try

            {

                wresp = await wr.GetResponseAsync();

                Stream stream2 = wresp.GetResponseStream();

                StreamReader reader2 = new StreamReader(stream2);

                string imageUrl = reader2.ReadToEnd();

                Debug.WriteLine(imageUrl);

                ImageControl.Source = new BitmapImage(new Uri(imageUrl, UriKind.Absolute));

                ImageUrl = imageUrl;

            }

            catch (Exception ex)

            {

                Debug.WriteLine("Error uploading file", ex.StackTrace);

                Debug.WriteLine("Error uploading file", ex.InnerException);

                if (wresp != null)

                {

                    wresp = null;

                }

            }

            finally

            {

                wr = null;

            }

        }
    }
}
