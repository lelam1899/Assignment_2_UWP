using Assignment2_UWP.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2_UWP.Service
{
    interface ISongService
    {

        Song PostSongFree(Song song);

        ObservableCollection<Song> GetFreeSongs();
        ObservableCollection<Song> GetMySongs();
    }
}
