using System;
using Windows.ApplicationModel.Background;
using Windows.Storage;
using Windows.UI.Notifications;
using TilesAndNotifications.Library;

namespace BackgroundTasks
{
    public sealed class ToastUpdateTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();

            var details = taskInstance.TriggerDetails as ToastNotificationActionTriggerDetail;
            if (details != null)
            {
                string arguments = details.Argument;
                // this is where you would retreive any user input
                var userInput = details.UserInput;

                var json = await ToDoTaskFileHelper.ReadToDoTaskJsonAsync();
                var task = ToDoTask.FromJson(json);

                task.IsComplete = arguments == "yes";

                await ToDoTaskFileHelper.SaveToDoTaskJson(task.ToJson());
            }

            deferral.Complete();
        }
    }
}