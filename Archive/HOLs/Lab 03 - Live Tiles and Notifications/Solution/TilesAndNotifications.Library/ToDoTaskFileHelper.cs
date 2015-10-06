using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace TilesAndNotifications.Library
{
    public static class ToDoTaskFileHelper
    {
        private static readonly string Filename = "task.json";

        public static async Task<string> ReadToDoTaskJsonAsync()
        {
            // declare an empty variable to be filled later
            string json = null;
            // define where the file resides
            var localfolder = ApplicationData.Current.LocalFolder;
            // see if the file exists
            if (await localfolder.TryGetItemAsync(Filename) != null)
            {
                // open the file
                var textfile = await localfolder.GetFileAsync(Filename);
                // read the file
                json = await FileIO.ReadTextAsync(textfile);
            }
            // if the file doesn't exist, we'll copy the app copy to local storage
            else
            {
                var storageFile =
                    await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Assets/task.json"));
                await storageFile.CopyAsync(ApplicationData.Current.LocalFolder);
                json = await FileIO.ReadTextAsync(storageFile);
            }

            return json;
        }

        public static async Task SaveToDoTaskJson(string json)
        {
            var localfolder = ApplicationData.Current.LocalFolder;
            var textfile = await localfolder.GetFileAsync(Filename);
            await FileIO.WriteTextAsync(textfile, json);
        }
    }
}
