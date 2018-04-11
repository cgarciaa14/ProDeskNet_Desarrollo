#region TRACKERS
//BBV-P-423-RQAUTBIO-01:MPUESTO:27/06/2017:Autenticación Biometría (INE)
//BBV-P-423 RQ-PD-17 2 GVARGAS 02/01/2018 Mejoras carga huella
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FPScannerX64
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new CaptureFinger());
            }
            catch (Exception) { }
        }

        public static void RunAndGo(string filePath)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.Run(new CaptureFinger(filePath));
            }
            catch (Exception) { }
        }
    }
}
