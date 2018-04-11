#region TRACKERS
//BBV-P-423-RQAUTBIO-01:MPUESTO:27/06/2017:Autenticación Biometría (INE)
//BUG-PD-229 GVARGAS 09/10/2017 Captura Hand Position & FingerPrint WSQ
//BBV-P-423 RQ-PD-17 2 GVARGAS 02/01/2018 Mejoras carga huella
//BBV-P-423 RQ-PD-17 3 GVARGAS 08/01/2018 Mejoras carga huella y cambio payload
#endregion

using Dermalog.AFIS.FourprintSegmentation;
using Dermalog.Imaging.Capturing;
using Dermalog.Afis.ImageIO;
using Dermalog.Afis.ImageIO.Enums;
using FPScannerX64.FPScannerClasses;
using ProdeskNet.Criptografia;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Script.Serialization;
using System.Web;

namespace FPScannerX64
{
    public partial class CaptureFinger : Form
    {
        #region Delegates
        delegate void SetTextCallback(string text);
        #endregion

        #region Variables
        private readonly DeviceIdentity _DeviceIdentity = DeviceIdentity.FG_ZF1;
        private bool _displayOnImage = true;
        JavaScriptSerializer _JavaScriptSerializer;
        #endregion

        #region Properties
        public FPScanner FingerPrintScanner { get; internal set; }
        private HandPositions _hand { get; set; }
        public Bitmap LeftBmp { get; internal set; }
        private byte[] ByteArray { get; set; }
        private Entities.FingerprintParams _FingerprintParams { get; set; }
        string _filePath { get; set; }
        #endregion

        #region Constants for Window Draggable
        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;
        #endregion

        public CaptureFinger()
        {
            this.MinimumSize = new Size(469, 733);
            this.MaximumSize = new Size(469, 733);

            MessageBox.Show("Es necesario un archivo con extensión .fng");
            InitializeComponent();
            this.Close();
        }

        public CaptureFinger(string filePath)
        {
            this.MinimumSize = new Size(469, 733);
            this.MaximumSize = new Size(469, 733);

            InitializeComponent();
            _filePath = filePath;
            DecryptFile();
        }

        private void DecryptFile()
        {
            _JavaScriptSerializer = new JavaScriptSerializer();
            responseFNG respFNG = _JavaScriptSerializer.Deserialize<responseFNG>(File.ReadAllText(_filePath));
            _FingerprintParams = _JavaScriptSerializer.Deserialize<Entities.FingerprintParams>(new Descifrado().DecryptString(respFNG.informationFNG, respFNG.PublicKey));
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Cifrado jojojoCifrado = new Cifrado();
            Descifrado jojojoDescifrado = new Descifrado();
            string jojojoaux = string.Empty;
            string strGuid = Guid.NewGuid().ToString();
            jojojoaux = jojojoCifrado.EncryptString("jojojo", strGuid);

            jojojoaux = jojojoDescifrado.DecryptString(jojojoaux, strGuid);


            DeviceInformations[] availableDevices = FPScanner.GetAttachedDevices(_DeviceIdentity);
            if (availableDevices == null || availableDevices.Length > 0)
            {
                FingerPrintScanner = FPScanner.GetFPScanner(_DeviceIdentity, availableDevices[0].index);
                btnCaptureFinger.Enabled = true;
            }
            else
            {
                btnCaptureFinger.Enabled = false;
                SetText("NO SE RECONOCE DISPOSITIVO CONECTADO");
                Task.Factory.StartNew(() =>
                {
                    System.Threading.Thread.Sleep(4000);
                    Application.Exit();
                });
            }
            this.Width = Convert.ToInt32(Math.Truncate(pbxFingerprint.Width * 1.081));
            this.Height = Convert.ToInt32(Math.Truncate(pbxFingerprint.Height * 1.432));
        }

        private void btnCaptureFingerprint_Click(object sender, EventArgs e)
        {
            if (((Button)sender).Name.ToUpper().Contains("LEFT"))
            {
                _hand = HandPositions.Left;
            }
            else
            {
                _hand = HandPositions.Right;
            }
            SetText("EN ESPERA DE LECTURA DE DEDO");
            StartCapturing();


            btnCaptureFinger.Enabled = false;
            btnSaveFinger.Enabled = true;
        }

        #region IFPScanner Events
        private void BindFPScannerEvents()
        {
            UnbindFPScannerEvents();

            FingerPrintScanner.OnScannerImage += FingerPrintScanner_OnScannerImage;
            FingerPrintScanner.OnScannerDetect += FingerPrintScanner_OnScannerDetect;
            FingerPrintScanner.OnScannerError += FingerPrintScanner_OnScannerError;
            FingerPrintScanner.OnFingerprintsDetected += FingerPrintScanner_OnFingerprintsDetected;
        }

        private void UnbindFPScannerEvents()
        {
            FingerPrintScanner.OnScannerImage -= FingerPrintScanner_OnScannerImage;
            FingerPrintScanner.OnScannerDetect -= FingerPrintScanner_OnScannerDetect;
            FingerPrintScanner.OnScannerError -= FingerPrintScanner_OnScannerError;
            FingerPrintScanner.OnFingerprintsDetected -= FingerPrintScanner_OnFingerprintsDetected;
        }

        void FingerPrintScanner_OnScannerError(object sender, FPScanner.ScannerErrorEventArgs e)
        {
            MessageBox.Show(e.Error);
        }

        void FingerPrintScanner_OnScannerImage(System.Drawing.Image image)
        {
            if (!_displayOnImage)
                return;

            LeftBmp = new Bitmap(image);
            pbxFingerprint.Image = LeftBmp;
        }

        void FingerPrintScanner_OnScannerDetect(System.Drawing.Image image)
        {
            SetText("EXTRAYENDO INFORMACION");

            Bitmap bmp = new Bitmap(image);
            pbxFingerprint.Image = bmp;
        }

        void FingerPrintScanner_OnFingerprintsDetected(List<Fingerprint> fingerprints)
        {
            FingerPrintScanner.StopCapturing();
            Bitmap bmp;
            MemoryStream _MemoryStream = new MemoryStream();
            foreach (Fingerprint fingerprint in fingerprints)
            {
                byte[] pBufferWSQInputImage = ImageToByteArray(fingerprint.Image);
                byte[] pBufferWSQOutImage;
                DermalogImageIO.ConvertImage(pBufferWSQInputImage, EFormat.eWSQ, "ratio=8", out pBufferWSQOutImage);
                _FingerprintParams.FingerprintImageWSQ = Convert.ToBase64String(pBufferWSQOutImage);

                bmp = new Bitmap(fingerprint.Image);
                bmp.Save(_MemoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                ByteArray = _MemoryStream.ToArray();
                _FingerprintParams.FingerprintImage = Convert.ToBase64String(ByteArray);
            }
            SetText("INFORMACION MANO " + (rbtLeft.Checked ? "IZQUIERDA" : "DERECHA") + " CAPTURADA");
            _FingerprintParams.hand = (rbtLeft.Checked ? ProdeskNet.Criptografia.Entities.HandPosition.Left : ProdeskNet.Criptografia.Entities.HandPosition.Right);

            MessageBox.Show("Lectura procesada correctamente");
            
            //SendRequest();
        }

        public byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }


        #endregion

        private void SendRequest()
        {
            //string postData;
            ////byte[] _byteArray;

            //_JavaScriptSerializer = new JavaScriptSerializer();
            //WebRequest request = WebRequest.Create(new Uri(_FingerprintParams.WebPage));

            //postData = _JavaScriptSerializer.Serialize(_FingerprintParams);
            //postData = new Cifrado().EncryptString(postData, Path.GetFileNameWithoutExtension(_filePath));
            //postData = string.Format("publicKey={0}&EncryptedData={1}", Path.GetFileNameWithoutExtension(_filePath), System.Web.HttpUtility.UrlEncode(postData));

            //MessageBox.Show("Lectura procesada correctamente");

            //_byteArray = Encoding.ASCII.GetBytes(postData);
            //request.Method = "POST";
            //request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentLength = _byteArray.Length;

            //using (var dataStream = request.GetRequestStream())
            //{
            //    dataStream.Write(_byteArray, 0, _byteArray.Length);
            //}

            //using (var response = (HttpWebResponse)request.GetResponse())
            //{
            //    StreamReader respStrm = new StreamReader(response.GetResponseStream(), Encoding.ASCII);
            //    var result = respStrm.ReadToEnd();
                
            //    if (!result.ToUpper().Contains("UPDATE"))
            //    {
            //        MessageBox.Show(result + "\r\n\r\n Inténtelo de nuevo mas tarde con el mismo archivo");
            //    }
            //    else
            //    {
            //        MessageBox.Show("Lectura procesada correctamente");
            //        try
            //        {
            //            File.Delete(_filePath);
            //            this.Close();
            //        }
            //        catch (Exception)
            //        {

            //            MessageBox.Show("No se ha podido eliminar el archivo " + _filePath);
            //        }
            //    }
            //}
        }

        public void StartCapturing()
        {
            BindFPScannerEvents();
            FingerPrintScanner.StartCapturing();
        }

        public void StopCapturing()
        {
            UnbindFPScannerEvents();
            FingerPrintScanner.StopCapturing();
        }

        private void SetText(string text)
        {
            if (this.tbxMessage.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.tbxMessage.Text = text;
            }
        }

        private void btnSaveFinger_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                String savePath = fbd.SelectedPath.ToString();

                _JavaScriptSerializer = new JavaScriptSerializer();

                String postData = _JavaScriptSerializer.Serialize(_FingerprintParams);
                postData = new Cifrado().EncryptString(postData, _FingerprintParams.Key);
                
                responseFinger _responseFinger = new responseFinger();
                _responseFinger.publicKey = _FingerprintParams.Key;
                _responseFinger.EncryptedData = postData;

                String response = _JavaScriptSerializer.Serialize(_responseFinger);

                try
                {
                    File.WriteAllText(savePath + @"\" + _FingerprintParams.Key + ".json", response);

                    MessageBox.Show("Archivo guardado correctamente en: " + savePath + @"\" + _FingerprintParams.Key);

                    File.Delete(_filePath);
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("No se ha podido eliminar el archivo " + _filePath);
                }
            } else {
                MessageBox.Show("Debe elegir una carpeta para guardar la captura.");
            }
        }

        public class responseFinger { 
            public String publicKey { get; set; }
            public String EncryptedData { get; set; }
        }

        public class responseFNG {
            public String PublicKey { get; set; }
            public String informationFNG { get; set; }
        }
    }
}
