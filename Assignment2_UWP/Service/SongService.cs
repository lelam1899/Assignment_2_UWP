using Assignment2_UWP.Constant;
using Assignment2_UWP.Entity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2_UWP.Service
{
    class SongService : ISongService

    {
        public Song PostSongFree(Song song)
        {
            var content = new StringContent(JsonConvert.SerializeObject(song), Encoding.UTF8, "application/json");

            HttpClient httpClient = new HttpClient();

            CreateReadFile createReadFile = new CreateReadFile();

            var token = createReadFile.GetToken();

            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);

            var response = httpClient.PostAsync(ApiUrl.API_SONG, content).Result.Content.ReadAsStringAsync().Result;

            Debug.WriteLine("Response:" + response);

            if (response.IndexOf("id") < 0)
            {

                Debug.WriteLine("Errors!");
                return null;

            }
            return song;
        }

        public ObservableCollection<Song> GetFreeSongs()
        {

            ObservableCollection<Song> songs = new ObservableCollection<Song>();

            var httpClient = new HttpClient();

            CreateReadFile createReadFile = new CreateReadFile();

            var token = createReadFile.GetToken();

            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);

            var responseContent = httpClient.GetAsync(ApiUrl.API_SONG).Result.Content.ReadAsStringAsync().Result;

            Debug.WriteLine("Response:" + responseContent);

            songs = JsonConvert.DeserializeObject<ObservableCollection<Song>>(responseContent);
            return songs;
        }

        public ObservableCollection<Song> GetMySongs()
        {

            ObservableCollection<Song> mysongs = new ObservableCollection<Song>();

            var httpClient = new HttpClient();

            CreateReadFile createReadFile = new CreateReadFile();

            var token = createReadFile.GetToken();

            httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + token);

            var responseContent = httpClient.GetAsync(ApiUrl.GET_MINE_SONG_URL).Result.Content.ReadAsStringAsync().Result;

            Debug.WriteLine("Response: " + responseContent);

            mysongs = JsonConvert.DeserializeObject<ObservableCollection<Song>>(responseContent);

            return mysongs;
        }
    }
}
