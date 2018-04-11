#region TRACKERS
//BBV-P-423-RQAUTBIO-01:MPUESTO:27/06/2017:Autenticación Biometría (INE)
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Dermalog.AFIS.FourprintSegmentation;

namespace FPScannerX64.FPScannerClasses
{
    public class Fingerprint : IDisposable
    {
        public Image Image
        {
            get;
            set;
        }

        public int NFIQ
        {
            get;
            set;
        }

        private HandPositions _hand = HandPositions.Unknown;
        public HandPositions Hand
        {
            get { return _hand; }
            set { _hand = value; }
        }

        public uint Position
        {
            get;
            set;
        }

        public void Dispose()
        {
            this.Image = null;
            this.NFIQ = 0;
            this._hand = HandPositions.Unknown;
        }
    }
}
