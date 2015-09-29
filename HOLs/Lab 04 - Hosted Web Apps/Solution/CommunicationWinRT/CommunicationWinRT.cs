using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

namespace CommunicationWinRT
{
    [Windows.Foundation.Metadata.AllowForWeb]
    public sealed class CommunicationWinRT
    {
        public async void toastMessage(String message, int delay)
        {
            ToastTemplateType toastTemplate = ToastTemplateType.ToastText01;
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);

            XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(message));

            ToastNotification toast = new ToastNotification(toastXml);

            await Task.Delay(delay);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
