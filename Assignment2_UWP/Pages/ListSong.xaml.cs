using Assignment2_UWP.Entity;
using Assignment2_UWP.Service;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Assignment2_UWP.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListSong : Page
    {
        private ISongService songService;

        private bool _isPlaying;

        private int _currentIndex = 0;

        ObservableCollection<Song> Songs;

        public ListSong()

        {
            this.InitializeComponent();

            songService = new SongService();

            LoadSongs();
        }

        private void LoadSongs()

        {
            Songs = songService.GetFreeSongs();
            ListViewSong.ItemsSource = Songs;

            _currentIndex = 0;
        }


        private void SelectSong(object sender, TappedRoutedEventArgs e)

        {

            var selectItem = sender as TextBlock;

            MyMediaPlayer.Pause();

            if (selectItem != null)

            {

                if (selectItem.Tag is Song currentSong)

                {

                    _currentIndex = Songs.IndexOf(currentSong);

                    MyMediaPlayer.Source = new Uri(currentSong.link);

                    Play();

                }

            }

        }

        private void ListView_DoubleTapped(object sender, Windows.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            var selectItem = ListViewSong.SelectedItem as Song;

            MyMediaPlayer.Pause();

            if (selectItem != null)

            {

                _currentIndex = Songs.IndexOf(selectItem);

                MyMediaPlayer.Source = new Uri(selectItem.link);

                SongThumbnail.ImageSource = new BitmapImage(new Uri(selectItem.thumbnail));

                Play();

            }

        }
        private void StatusButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (_isPlaying)

            {

                Pause();

            }

            else

            {

                Play();

            }

        }
        private void Play()
        {
            MyMediaPlayer.Source = new Uri(Songs[_currentIndex].link);

            ControlLabel.Text = "Now Playing: " + Songs[_currentIndex].name;

            ListViewSong.SelectedIndex = _currentIndex;

            MyMediaPlayer.Play();

            StatusButton.Icon = new SymbolIcon(Symbol.Pause);

            _isPlaying = true;
        }

        private void Pause()
        {
            ControlLabel.Text = "Pause";

            MyMediaPlayer.Pause();

            StatusButton.Icon = new SymbolIcon(Symbol.Play);

            _isPlaying = false;
        }

        private void PreviousButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _currentIndex--;

            if (_currentIndex < 0)

            {

                _currentIndex = Songs.Count - 1;

            }
            else if (_currentIndex >= Songs.Count)

            {

                _currentIndex = 0;

            }

            Play();

        }


        private void NextButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _currentIndex++;

            if (_currentIndex >= Songs.Count || _currentIndex < 0)

            {

                _currentIndex = 0;

            }

            Play();

        }
    }
}
