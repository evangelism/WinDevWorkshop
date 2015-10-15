using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.AppService;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AppServices
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private AppServiceConnection appServiceConnection;
        public ObservableCollection<Employee> Items { get; set; } = new ObservableCollection<Employee>();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void GetEmployeeById(object sender, RoutedEventArgs e)
        {

            appServiceConnection = new AppServiceConnection
            {
                AppServiceName = "EmployeeLookupService",
                PackageFamilyName = "3598a822-2b34-44cc-9a20-421137c7511f_4frctqp64dy5c"
            };

            var status = await appServiceConnection.OpenAsync();

            switch (status)
            {
                case AppServiceConnectionStatus.AppNotInstalled:
                    await LogError("The EmployeeLookup application is not installed. Please install it and try again.");
                    return;
                case AppServiceConnectionStatus.AppServiceUnavailable:
                    await LogError("The EmployeeLookup application does not have the available feature");
                    return;
                case AppServiceConnectionStatus.AppUnavailable:
                    await LogError("The package for the app service to which a connection was attempted is unavailable.");
                    return;
                case AppServiceConnectionStatus.Unknown:
                    await LogError("Unknown Error.");
                    return;
            }

            var items = this.EmployeeId.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var message = new ValueSet();

            for (int i = 0; i < items.Length; i++)
            {
                message.Add(i.ToString(), items[i]);
            }

            var response = await appServiceConnection.SendMessageAsync(message);

            switch (response.Status)
            {
                case AppServiceResponseStatus.ResourceLimitsExceeded:
                    await LogError("Insufficient resources. The app service has been shut down.");
                    return;
                case AppServiceResponseStatus.Failure:
                    await LogError("Failed to receive response.");
                    return;
                case AppServiceResponseStatus.Unknown:
                    await LogError("Unknown error.");
                    return;
            }

            foreach (var item in response.Message)
            {
                this.Items.Add(new Employee
                {
                    Id = item.Key,
                    Name = item.Value.ToString()
                });
            }
        }

        private async Task LogError(string errorMessage)
        {
            await new MessageDialog(errorMessage).ShowAsync();
        }
    }
}


