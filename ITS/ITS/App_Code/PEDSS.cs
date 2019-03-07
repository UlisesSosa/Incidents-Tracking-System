using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Net;
using System.Net.Mail;


/// <summary>
/// Summary description for PEDSS v 1.0
/// </summary>
public class PEDSS
{
    public class Crypto
    {
        #region Class fields
        // Encryption hash key.
        static readonly string PasswordHash = "P@@Sw0rd?@#";
        // Encryption salt key.
        static readonly string SaltKey = "PLOI5NAGH542!";
        // Encryption vector key.
        static readonly string VIKey = "@1B2c3D4e5F6g7H8";
        #endregion

        #region Public Methods
        /// <summary>
        /// Encyption method.
        /// </summary>
        /// <param name="text">string</param>
        /// <returns>string</returns>
        /// Developer: Pedro Salazar
        /// Created: 02/08/17
        public static string EncryptString(string text)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(text);

            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

            byte[] cipherTextBytes;

            using (var memoryStream = new MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                }
                memoryStream.Close();
            }

            return Convert.ToBase64String(cipherTextBytes);
        }

        /// <summary>
        /// Decryption method.
        /// </summary>
        /// <param name="text">string</param>
        /// <returns>string</returns>
        /// Developer: Pedro Salazar
        /// Created: 02/08/17
        public static string DecryptString(string text)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(text);
            byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
            var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

            var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

            memoryStream.Close();
            cryptoStream.Close();

            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
        }

        /// <summary>
        /// Calculates MD5 hash value of string.
        /// </summary>
        /// <param name="input">string</param>
        /// <returns>string</returns>
        /// Developer: Pedro Salazar
        /// Created: 02/08/17
        public static string GetMD5Hash(string input)
        {
            MD5 md5 = MD5.Create();
            string HashValue = String.Empty;

            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            HashValue = sb.ToString();
            sb.Clear();

            return HashValue;
        }
        #endregion
    }

    public class Errorlog
    {
        /// <summary>
        /// Registra los errores en una base de datos
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="Form"></param>
        /// <param name="Method"></param>
        /// <param name="StackTrace"></param>
        /// <param name="IP"></param>
        /// <param name="UserAgent"></param>
        public static void Register(string Message,string Form,string Method,string StackTrace, string IP, string UserAgent)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString);
            SqlCommand comm = new SqlCommand("AppErrorLog", conn);
            comm.CommandType = CommandType.StoredProcedure;
            comm.Parameters.AddWithValue("@Message", Message);
            comm.Parameters.AddWithValue("@Form", Form);
            comm.Parameters.AddWithValue("@Method", Method);
            comm.Parameters.AddWithValue("@StackTrace", StackTrace);
            comm.Parameters.AddWithValue("@IP", IP);
            comm.Parameters.AddWithValue("@UserAgent", UserAgent);

            try
            {
                conn.Open();
                comm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
        }
    }


    public class General
    {
        /// <summary>
        /// Estructura de un archivo cargado
        /// </summary>
        public class FIleToUpload
        {
            public string Name { get; set; }
            public string Mime { get; set; }
            public int Size { get; set; }
            public byte[] FileByte { get; set; }

            public FIleToUpload(string name, string mime, int size, byte[] fileByte)
            {
                Name = name;
                Mime = mime;
                Size = size;
                FileByte = fileByte;
            }
            //Other properties, methods, events...
        }

        /// <summary>
        /// Estructura de un objeto que guarda la informacion SMTP para envio de correos
        /// </summary>
        public class SMTP
        {
            public string DisplayName
            {
                get;
                set;
            }

            public string Email
            {
                get;
                set;
            }

            public string Password
            {
                get;
                set;
            }

            public int SMTPPort
            {
                get;
                set;
            }

            public string SMTPServer
            {
                get;
                set;
            }

            public bool SMTPSSL
            {
                get;
                set;
            }

            public SMTP()
            {
            }

            public SMTP(string smtpserver, int smtpport, string email, bool smtpssl, string password, string displayname)
            {
                SMTPServer = smtpserver;
                SMTPPort = smtpport;
                Email = email;
                SMTPSSL = smtpssl;
                Password = password;
                DisplayName = displayname;
            }


            /// <summary>
            /// Carga la informacion de la base de datos y crea un objeto SMTP
            /// </summary>
            /// <returns></returns>
            public static SMTP GetSMTPSettings()
            {
                SMTP SMTP = new SMTP();
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString);
                SqlCommand comm = new SqlCommand("GetSettings", conn);
                comm.CommandType = CommandType.StoredProcedure;
                try
                {
                    conn.Open();
                    SqlDataReader sqlDataReader = comm.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            SMTP.SMTPServer = HttpContext.Current.Server.HtmlDecode(sqlDataReader["smtpserver"].ToString());
                            SMTP.SMTPSSL = Convert.ToBoolean(HttpContext.Current.Server.HtmlDecode(sqlDataReader["SMTPSSL"].ToString()));
                            SMTP.SMTPPort = Convert.ToInt32(HttpContext.Current.Server.HtmlDecode(sqlDataReader["SMTPPort"].ToString()));
                            SMTP.DisplayName = HttpContext.Current.Server.HtmlDecode(sqlDataReader["companyname"].ToString());
                            SMTP.Email = HttpContext.Current.Server.HtmlDecode(sqlDataReader["SMTPEmail"].ToString());
                            SMTP.Password = HttpContext.Current.Server.HtmlDecode(sqlDataReader["EmailPassword"].ToString());
                        }
                    }
                    sqlDataReader.Close();
                }
                catch (Exception ex)
                {
                    PEDSS.Errorlog.Register(ex.Message, "Local", "GetSMTPSettings", ex.StackTrace, HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Request.UserAgent);
                }
                finally
                {
                    conn.Close();
                }
                return SMTP;
            }
        }

        /// <summary>
        /// Enva un correo electronico
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="html"></param>
        /// <param name="txt"></param>
        /// <param name="To"></param>
        /// <returns></returns>
        public static bool SendEmail(string subject, string html, string txt, string To)
        {
            SMTP sMTP = new SMTP();
            sMTP = SMTP.GetSMTPSettings();
            bool flag = false;
            string sMTPServer = sMTP.SMTPServer;
            int sMTPPort = sMTP.SMTPPort;
            string Email = PEDSS.Crypto.DecryptString(sMTP.Email);
            string Password = PEDSS.Crypto.DecryptString(sMTP.Password);
            bool sMTPSSL = sMTP.SMTPSSL;
            string displayName = sMTP.DisplayName;
            MailMessage mailMessage = new MailMessage();
            MailAddress mailAddress = new MailAddress(Email, displayName, Encoding.UTF8);
            MailAddress mailAddress1 = new MailAddress(To);
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.SubjectEncoding = Encoding.UTF8;
            mailMessage.HeadersEncoding = Encoding.UTF8;
            mailMessage.From = mailAddress;
            mailMessage.To.Add(mailAddress1);

            mailMessage.Subject = subject;
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(txt, Encoding.ASCII, "text/plain");
            AlternateView alternateView1 = AlternateView.CreateAlternateViewFromString(html, Encoding.UTF8, "text/html");
            mailMessage.AlternateViews.Add(alternateView);
            mailMessage.AlternateViews.Add(alternateView1);
            SmtpClient smtpClient = new SmtpClient()
            {
                Host = sMTPServer,
                Port = sMTPPort,
                Credentials = new NetworkCredential(Email, Password),
                EnableSsl = sMTPSSL
            };
            try
            {
                smtpClient.Send(mailMessage);
                smtpClient.Dispose();
                flag = true;
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                PEDSS.Errorlog.Register(exception.Message, "Local", "SendEmail", exception.StackTrace, HttpContext.Current.Request.UserHostAddress, HttpContext.Current.Request.UserAgent);
            }
            return flag;
        }

       
        private static Random rnd;

        static General()
        {
            PEDSS.General.rnd = new Random();
        }


        /// <summary>
        /// Corta una cadena de texto a la longitud indicada
        /// </summary>
        /// <param name="Word">Palabra a cortar</param>
        /// <param name="Length">Longitud maxima</param>
        /// <returns>Cadena de texto cortada</returns>
        /// Developer: Pedro Salazar
        /// Date:12/10/17
        public static string CutString(string Word, int Length)
        {
            if (Word.Length > Length)
            {
                Word = Word.Substring(0, Length);
                Word = string.Concat(Word, "...");
            }
            return Word;
        }

        /// <summary>
        /// Genera una palabra random alfanumerica
        /// </summary>
        /// <param name="Size">Longitud de la palabra</param>
        /// <returns>Palabra generada</returns>
        /// Developer: Pedro Salazar
        /// Date:12/10/17
        public static string GenerateRandomAlfaNumericString(int Size = 6)
        {
            string str = "";
            string[] strArrays = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            string[] strArrays1 = strArrays;
            for (int i = 1; i <= Size; i++)
            {
                str = string.Concat(str, strArrays1[PEDSS.General.rnd.Next((int)strArrays1.Length)]);
            }
            return str;
        }

        /// <summary>
        /// Genera una palabra random solo de letras
        /// </summary>
        /// <param name="Size">Longitud de la palabra</param>
        /// <returns>Palabra generada</returns>
        /// Developer: Pedro Salazar
        /// Date:12/10/17
        public static string GenerateRandomAlfaString(int Size = 6)
        {
            string str = "";
            string[] strArrays = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            string[] strArrays1 = strArrays;
            for (int i = 1; i <= Size; i++)
            {
                str = string.Concat(str, strArrays1[PEDSS.General.rnd.Next((int)strArrays1.Length)]);
            }
            return str;
        }

        /// <summary>
        /// Genera una palabra random solo de numeros
        /// </summary>
        /// <param name="Size">Longitud de la palabra</param>
        /// <returns>Palabra generada</returns>
        /// Developer: Pedro Salazar
        /// Date:12/10/17
        public static string GenerateRandomNumericString(int Size = 6)
        {
            string str = "";
            string[] strArrays = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string[] strArrays1 = strArrays;
            for (int i = 1; i <= Size; i++)
            {
                str = string.Concat(str, strArrays1[PEDSS.General.rnd.Next((int)strArrays1.Length)]);
            }
            return str;
        }

        /// <summary>
        /// Genera una zona horaria de acuerdo al id proporcionado
        /// </summary>
        /// <param name="TimeZoneID"></param>
        /// <returns>TimeZoneInfo</returns>
        /// Developer: Pedro Salazar
        /// Date:12/10/17
        public static TimeZoneInfo GetTimeZone(int TimeZoneID)
        {
            return TimeZoneInfo.GetSystemTimeZones()[TimeZoneID];
        }

        /// <summary>
        /// Da formato de moneda a un numero decimal (56.99)
        /// </summary>
        /// <param name="Money"></param>
        /// <returns>string con formato de moneda</returns>
        /// Developer: Pedro Salazar
        /// Date:12/10/17
        public static string FormatMoney(decimal Money)
        {

            string MoneyFormated = "";
            MoneyFormated = Money.ToString("#,##0.00");

            return MoneyFormated;
        }

        /// <summary>
        /// Valida el ID de la divisa proporcionada
        /// </summary>
        /// <param name="CurrencyID"></param>
        /// <returns>Nombre correspondiente de la divisa</returns>
        /// Developer: Pedro Salazar
        /// Date:12/10/17
        public static string ValCurrency(int CurrencyID)
        {
            string CurrencyName = "";
            if (CurrencyID == 1)
            {
                CurrencyName = "MXN";
            }
            else if (CurrencyID == 2)
            {
                CurrencyName = "USD";
            }
            return CurrencyName;
        }


       
    }

    
}