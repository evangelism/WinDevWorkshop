using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace BackgroundTasks
{
    public sealed class TileUpdateTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();

            // Update the primary tile
            //var adaptiveTileHelper = new AdaptiveTileHelper();

            //adaptiveTileHelper.UpdatePrimaryTile(SolarizrTiles.CreatePrimaryTile(remainingAppointments));

            deferral.Complete();
        }
    }
}
