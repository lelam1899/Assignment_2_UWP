using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using Windows.Foundation;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Assignment2_UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FileHandle : Page
    {
        private StorageFile photo;
        private object ImageUrl;

        public FileHandle()

        {

            this.InitializeComponent();

            // get upload url.

        }





        public async void HttpUploadFile(string url, string paramName, string contentType)

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

        private async void ButtonBase_OnClick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

            // get upload url

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
    }
}
