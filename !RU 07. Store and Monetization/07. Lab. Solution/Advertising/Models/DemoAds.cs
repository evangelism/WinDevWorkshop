using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advertising.Models
{
    /*
           These demo ad values are drawn from: https://msdn.microsoft.com/en-US/library/mt125365(v=msads.100).aspx
       */
    public static class DemoAds
    {
        public static Dictionary<string, AdUnit> ImageAdUnits { get; private set; }
        public static AdUnit VideoAdUnit { get; private set; }

        static DemoAds()
        {
            ImageAdUnits = new Dictionary<string, AdUnit>();

            ImageAdUnits.Add("300 x 50",
              new AdUnit { Size = "300 x 50", AdUnitId = "10865275", AppId = "3f83fe91-d6be-434d-a0ae-7351c5a997f1" });
            ImageAdUnits.Add("320 x 50",
              new AdUnit { Size = "320 x 50", AdUnitId = "10865270", AppId = "3f83fe91-d6be-434d-a0ae-7351c5a997f1" });
            ImageAdUnits.Add("300 x 250",
              new AdUnit { Size = "300 x 250", AdUnitId = "10043121", AppId = "d25517cb-12d4-4699-8bdc-52040c712cab" });
            ImageAdUnits.Add("300 x 600",
              new AdUnit { Size = "300 x 600", AdUnitId = "10043122", AppId = "d25517cb-12d4-4699-8bdc-52040c712cab" });
            ImageAdUnits.Add("480 x 80",
              new AdUnit { Size = "480 x 80", AdUnitId = "10865272", AppId = "3f83fe91-d6be-434d-a0ae-7351c5a997f1" });
            ImageAdUnits.Add("640 x 100",
              new AdUnit { Size = "640 x 100", AdUnitId = "10865273", AppId = "3f83fe91-d6be-434d-a0ae-7351c5a997f1" });
            ImageAdUnits.Add("728 x 90",
              new AdUnit { Size = "728 x 90", AdUnitId = "10043123", AppId = "d25517cb-12d4-4699-8bdc-52040c712cab" });

            VideoAdUnit = new AdUnit { Size = "Video", AdUnitId = "11389925", AppId = "d25517cb-12d4-4699-8bdc-52040c712cab" };
        }
    }

    public class AdUnit
    {
        public string Size { get; set; }
        public string AdUnitId { get; set; }
        public string AppId { get; set; }
    }
}
