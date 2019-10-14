using Assignment2_UWP.Entity;
using Assignment2_UWP.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Assignment2_UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UpLoadSong : Page
    {

        private ISongService songService;

        public UpLoadSong()
        {
            this.InitializeComponent();

            songService = new SongService();
        }


        private void Button_UpLoad_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.ValidateName.Text = string.Empty;
            this.ValidateLink.Text = string.Empty;
            this.ValidateThumbnail.Text = string.Empty;


            if (this.Name.Text.Length == 0)
            {
                this.ValidateName.Text = "Vui long nhap...!";
                return;
            }
            if (this.Name.Text.Length > 50)
            {
                this.ValidateName.Text = "Toi da 50 ki tu !";
                return;
            }


            if (this.Thumbnail.Text == "")
            {
                this.ValidateThumbnail.Text = "Vui long nhap...!";
                return;
            }

            if (this.Link.Text == "")
            {
                this.ValidateLink.Text = "Vui long nhap...!";
                return;
            }
            if (!this.Link.Text.Contains(".mp3"))
            {
                this.ValidateLink.Text = "có chua .mp3";
                return;
            }

            Song song = new Song()
            {
                name = this.Name.Text,
                description = this.Description.Text,
                singer = this.Singer.Text,
                author = this.Author.Text,
                thumbnail = this.Thumbnail.Text,
                link = this.Link.Text,
            };


            songService.PostSongFree(song);

        }
    }
}
