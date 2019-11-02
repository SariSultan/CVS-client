using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;
using CVS.Common.Logging;

namespace CVS.ClientV1
{
    static class Program
    {
        public static FileLogger FileLogger;
        public static string ServerIp = @"CVS.calciumarabia.com";//this is the default, it can be changed in confing
        public static int ServerPort = 3333;
        public static string AuthKey = null;
        public static string DeviceId = null;
        public static X509Certificate2 ServerPublicKey = null;
        public static bool IsServerVerified => !(ServerIp == null || ServerPort == -1 || AuthKey == null ||
                                                 DeviceId == null);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {





            string logFileName = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) ?? throw new InvalidOperationException(),
                    "Logs", DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace("/", "_").Replace(":", "_") + ".CVS_CLIENT_LOG");
            if (!Directory.Exists(Path.GetDirectoryName(logFileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(logFileName) ?? throw new InvalidOperationException());
            StreamWriter writer = new StreamWriter(logFileName);
            try
            {
                FileLogger = new FileLogger(writer);
                ServerIp = System.Configuration.ConfigurationSettings.AppSettings["hostName"];
                if (ServerIp.ToLower().Contains(".com"))
                {
                    ServerIp = Dns.GetHostEntry(ServerIp).AddressList.FirstOrDefault(x => x.AddressFamily==AddressFamily.InterNetwork)?.ToString();
                }
                ServerPort = int.Parse(System.Configuration.ConfigurationSettings.AppSettings["hostPort"]);

                FileLogger.Log($"Started CVS server application", LogMsgType.Debug);

                //CHECK IF THE SERVER CERTIFICATE EXISTS 
                var certificateLocation = System.Configuration.ConfigurationSettings.AppSettings["serverCertificateFileName"];
                if (!File.Exists(certificateLocation))
                {
                    FileLogger.Log($"Cannot find server certificate at location [{certificateLocation}]", LogMsgType.Fatal);
                    return;
                }
                else
                {
                    ServerPublicKey = new X509Certificate2(certificateLocation);
                }
                Application.Run(new MainFrm());

                FileLogger.Log($"Ended CVS server application", LogMsgType.Debug);
            }
            catch (Exception ex)
            {
                FileLogger.Log($"Exception in main thread, trace=[{ex}]", LogMsgType.Exception);
                MessageBox.Show(ex.ToString(), @"Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                writer.Close();
            }


        }
    }
}
