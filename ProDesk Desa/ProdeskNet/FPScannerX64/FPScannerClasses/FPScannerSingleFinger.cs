#region TRACKERS
//BBV-P-423-RQAUTBIO-01:MPUESTO:27/06/2017:Autenticación Biometría (INE)
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Dermalog.Imaging.Capturing;
using Dermalog.Afis.NistQualityCheck;

namespace FPScannerX64.FPScannerClasses
{
    public abstract class FPScannerSingleFinger : FPScanner
    {
        public FPScannerSingleFinger(DeviceIdentity deviceIdentity, int index, CaptureMode captureMode)
            : base(deviceIdentity, index, captureMode)
        {
            setRedLed(true);
        }

        public void setLeds(bool enable)
        {
            setGreenLed(enable);
            setRedLed(enable);
        }

        #region SingleFinger-Scanner specific functions to implement
        public abstract void setGreenLed(bool enable);
        public abstract void setRedLed(bool enable);
        #endregion

        #region Implementation of abstract methods in base-class
        public override void StartCapturing()
        {
            base.Start();

            setGreenLed(true);
        }

        public override void StopCapturing()
        {
            base.Stop();

            setLeds(false);
            setRedLed(true);
        }

        protected override void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                setGreenLed(false);
                setRedLed(true);

                var localImage = e.Argument as System.Drawing.Image;
                int nQC = DermalogNistQualityCheck.Check(localImage);

                Fingerprint fp = new Fingerprint();
                fp.Image = localImage;
                //fp.Template = t1;
                fp.NFIQ = nQC;

                List<Fingerprint> fps = new List<Fingerprint>()
                {
                    fp
                };

                base.InvokeFingerprintsDetected(fps);

                setGreenLed(true);
                setRedLed(false);
            }
            catch (Exception ex)
            {
                InvokeScannerError(sender, new ScannerErrorEventArgs("Processing error: " + ex.Message, ex));
            }
        }
        #endregion
    }
}
