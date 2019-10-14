using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2_UWP.Service
{
    class CreateReadFile
    {
        public void SaveTokenToLocalStorage(string token)

        {

            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            Windows.Storage.StorageFile sampleFile = storageFolder.CreateFileAsync("sample.txt",

                Windows.Storage.CreationCollisionOption.ReplaceExisting).GetAwaiter().GetResult();

            Windows.Storage.FileIO.WriteTextAsync(sampleFile, token).GetAwaiter().GetResult();



        }


        public string GetToken()
        {

            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            Windows.Storage.StorageFile sampleFile = storageFolder.GetFileAsync("sample.txt").GetAwaiter().GetResult();

            string token = Windows.Storage.FileIO.ReadTextAsync(sampleFile).GetAwaiter().GetResult();

            return token;
        }
    }
}
