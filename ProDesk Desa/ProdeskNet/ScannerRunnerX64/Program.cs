#region TRACKERS
//BBV-P-423-RQAUTBIO-01:MPUESTO:27/06/2017:Autenticación Biometría (INE)
//BBV-P-423 RQ-PD-17 2 GVARGAS 02/01/2018 Mejoras carga huella
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScannerRunnerX64
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            FPScannerX64.Program.RunAndGo(args[0]);
        }
    }
}
