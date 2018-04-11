#region TRACKERS
//BBV-P-423-RQAUTBIO-01:MPUESTO:27/06/2017:Autenticación Biometría (INE)
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dermalog.Imaging.Capturing;

namespace FPScannerX64.FPScannerClasses
{
    public class FPScannerZF1 : FPScannerSingleFinger
    {
        public FPScannerZF1(DeviceIdentity id, int index, CaptureMode captureMode = CaptureMode.PREVIEW_IMAGE_AUTO_DETECT)
            : base(id, index, captureMode)
        {
        }

        #region Implementation of abstract methods in base-class
        public override void StartCapturing()
        {
            base.StartCapturing();
        }

        public override void StopCapturing()
        {
            base.StopCapturing();
        }

        public override void setGreenLed(bool enable)
        {
            int value = enable ? 1 : 0;
            base.setDeviceProperty(PropertyType.FG_GREEN_LED, value);
        }

        public override void setRedLed(bool enable)
        {
            int value = enable ? 1 : 0;
            base.setDeviceProperty(PropertyType.FG_RED_LED, value);
        }
        #endregion
    }
}
