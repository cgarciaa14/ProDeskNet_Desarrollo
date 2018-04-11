#region TRACKERS
//BBV-P-423-RQAUTBIO-01:MPUESTO:27/06/2017:Autenticación Biometría (INE)
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPScannerX64.FPScannerClasses
{
    public class DeviceInfo
    {
        private int mIndex;
        public int Index
        {
            get { return mIndex; }
            set { mIndex = value; }
        }

        private String mName;
        public String Name
        {
            get { return mName; }
            set { mName = value; }
        }

        public DeviceInfo(int index, string name)
        {
            this.Index = index;
            this.Name = name;
        }

        public override string ToString()
        {
            return mName;
        }
    }
}