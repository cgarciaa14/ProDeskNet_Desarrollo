#region TRACKERS
//BBV-P-423-RQAUTBIO-01:MPUESTO:27/06/2017:Autenticación Biometría (INE)
//BUG-PD-229 GVARGAS 09/10/2017 Captura Hand Position & FingerPrint WSQ
//BBV-P-423 RQ-PD-17 8 GVARGAS 29/01/2018 Ajuste validaciones Biometrico
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ProdeskNet.Criptografia
{
    public class Entities
    {
        public class ValidateIdentity
        {
            public class clsRequest
            {
                public string user { get; set; }
                public string fingerprintImage { get; set; }
                public CredentialNumber credentialNumber { get; set; }

                public class CredentialNumber
                {
                    public string number { get; set; }
                }
            }

            public class clsResponse
            {
                public class Person
                {
                    public IList<IdentityDocument> identityDocument { get; set; }
                    
                    public class IdentityDocument
                    {
                        public docType type { get; set; }

                        public class docType
                        {
                            public string id { get; set; }
                            public string name { get; set; }
                        }
                    }
                }
            }
        }

        public class FingerprintParams
        {
            public int IdPantalla { get; set; }
            public long NoSolicitud { get; set; }
            public int IdUsuario { get; set; }
            public String Key { get; set; }
            public long IdAutenticacion { get; set; }
            public String FingerprintImage { get; set; }
            public String WebPage { get; set; }
            public String Browser { get; set; }
            public HandPosition hand { get; set; }
            public String FingerprintImageWSQ { get; set; }
        }

        public enum HandPosition
        {
            Left,
            Right
        }
    }
}
