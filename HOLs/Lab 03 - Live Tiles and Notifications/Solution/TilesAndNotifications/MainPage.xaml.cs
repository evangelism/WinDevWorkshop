using System;
using System.ComponentModel;
using Windows.Data.Json;
using Windows.Storage;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using TilesAndNotifications.Library;
using TilesAndNotifications.Services;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TilesAndNotifications
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        private int _count;
        private ToDoTask _currentToDoTask;

        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        public ToDoTask CurrentToDoTask
        {
            get { return _currentToDoTask; }
            set
            {
                _currentToDoTask = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentToDoTask)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private async void Refresh()
        {
            var json = await ToDoTaskFileHelper.ReadToDoTaskJsonAsync();
            CurrentToDoTask = ToDoTask.FromJson(json);
        }

        private void UpdateBadge(object sender, RoutedEventArgs e)
        {
            _count++;
            TileService.SetBadgeCountOnTile(_count);
        }

        private void UpdatePrimaryTile(object sender, RoutedEventArgs e)
        {
            var xmlDoc = TileService.CreateTiles(new PrimaryTile());

            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            var notification = new TileNotification(xmlDoc);
            updater.Update(notification);
        }

        private void Notify(object sender, RoutedEventArgs e)
        {
            var xmlDoc = ToastService.CreateToast();
            var notifier = ToastNotificationManager.CreateToastNotifier();
            var toast = new ToastNotification(xmlDoc);
            notifier.Show(toast);
        }
    }
}