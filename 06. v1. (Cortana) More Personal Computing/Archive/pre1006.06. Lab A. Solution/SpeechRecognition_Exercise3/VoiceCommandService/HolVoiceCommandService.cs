using System;
using Windows.ApplicationModel.AppService;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.VoiceCommands;

namespace VoiceCommandService
{
    public sealed class HolVoiceCommandService : IBackgroundTask
    {
        VoiceCommandServiceConnection voiceServiceConnection;
        BackgroundTaskDeferral serviceDeferral;


        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            serviceDeferral = taskInstance.GetDeferral();

            taskInstance.Canceled += OnTaskCanceled;

            var triggerDetails = taskInstance.TriggerDetails as AppServiceTriggerDetails;

            if (triggerDetails != null && triggerDetails.Name == "HolVoiceCommandService")
            {
                try
                {
                    voiceServiceConnection =
                        VoiceCommandServiceConnection.FromAppServiceTriggerDetails(
                            triggerDetails);

                    voiceServiceConnection.VoiceCommandCompleted += OnVoiceCommandCompleted;

                    VoiceCommand voiceCommand = await voiceServiceConnection.GetVoiceCommandAsync();

                    switch (voiceCommand.CommandName)
                    {
                        case "SayHello":
                            var userMessage = new VoiceCommandUserMessage();
                            userMessage.DisplayMessage = "Hello!";
                            userMessage.SpokenMessage = "Your app says hi. It is having a great time.";
                            var response = VoiceCommandResponse.CreateResponse(userMessage);
                            await voiceServiceConnection.ReportSuccessAsync(response);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Handling Voice Command failed " + ex.ToString());
                }
            }

        }

        private void OnVoiceCommandCompleted(VoiceCommandServiceConnection sender, VoiceCommandCompletedEventArgs args)
        {
            if (this.serviceDeferral != null)
            {
                this.serviceDeferral.Complete();
            }
        }

        private void OnTaskCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {
            System.Diagnostics.Debug.WriteLine("Task cancelled, clean up");
            if (this.serviceDeferral != null)
            {
                //Complete the service deferral
                this.serviceDeferral.Complete();
            }
        }

    }
}
