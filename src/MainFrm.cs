using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CVS.Common.Communication;
using CVS.Common.Communication.API;
using CVS.Common.Information_Gathering;
using CVS.Common.Logging;
using CVS.Common.Reporting;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.SslScs.Authentication;
using Hik.Communication.SslScsServices.Client;

namespace CVS.ClientV1
{
    public partial class MainFrm : Form
    {
        public FileLogger ALogger { get; }

        private void UpdateProgress(string msg)
        {
            if (scanBtn.InvokeRequired)
            {
                scanBtn.BeginInvoke(new Action<string>(UpdateProgress), msg);
                return;
            }

            scanBtn.Text = msg;
        }
        public MainFrm()
        {
            InitializeComponent();


            ALogger = Program.FileLogger;
            versionLbl.Text = $@"Version: {Application.ProductVersion}";
            FormBorderStyle = FormBorderStyle.None;


            ExitPB.Click += Validators.ExitPBOnClick;
            ExitPB.MouseHover += Validators.ExitAndMinimizeMouseHover;
            ExitPB.MouseLeave += Validators.ExitAndMinimizeMouseLeave;
            MinimizePB.Click += Validators.MinimizePBOnClick;
            MinimizePB.MouseHover += Validators.ExitAndMinimizeMouseHover;
            MinimizePB.MouseLeave += Validators.ExitAndMinimizeMouseLeave;
        }

        private void Scan()
        {
            try
            {
                BlockUI();
                using (
                    //var client = ScsServiceClientBuilder.CreateClient<INetworkBVPSService>(
                    //    new ScsTcpEndPoint(Program.ServerIp, Program.ServerPort)
                    //)
                    var client = SslScsServiceClientBuilder.CreateSslClient<INetworkBVPSService>(
                new ScsTcpEndPoint(Program.ServerIp, Program.ServerPort)
                , Program.ServerPublicKey
                , Program.ServerIp
                , SslScsAuthMode.ServerAuth
                , null
                 )
                )
                {
                    client.Timeout = 10 * 60 * 1000; //timeout 10 minutes 
                    client.ConnectTimeout = 5 * 1000; //Time to connect // 5 seconds
                    client.Connect();
                    var details = new RequiredCommunicationDetails
                    {
                        DeviceID = devIdTxtBox.Text,
                        TimeStamp = DateTime.Now
                    };

                    var requiredDetails = new RequiredCommunicationDetailsPacket
                    {
                        DeviceID = devIdTxtBox.Text,
                        CommunicationDetails = details
                    };
                    requiredDetails.PrepareForSending(authKeyTxtBox.Text);

                    UpdateProgress($"Connecting...");
                    var verResult = client.ServiceProxy.VerifyConnection(requiredDetails);

                    if (verResult.Item1)
                    {
                        UpdateProgress($"Connection Verified Successfully.");
                        // MessageBox.Show(@"Connection verified successfully!");
                        Program.FileLogger.Log($"Verified to the server successfully", LogMsgType.Warning);

                        Program.DeviceId = devIdTxtBox.Text;
                        Program.AuthKey = authKeyTxtBox.Text;

                    }
                    else
                    {
                        UpdateProgress($"Connection Verification failed.");
                        MessageBox.Show($@"Not Verified, error ={verResult.Item2}");
                        Program.FileLogger.Log($"Could not verify to the server, the issue was {verResult.Item2}",
                            LogMsgType.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                UpdateProgress($"Error");
                MessageBox.Show(@"Exception [Cannot connect to the server] " + ex.Message);
                Program.FileLogger.Log(@"exception [most probably server is not running] " + ex.Message,
                    LogMsgType.Exception);
            }

            if (!Program.IsServerVerified)
            {
                EnableUI();
                return;
            }
            try
            {
                using (
                    //var client = ScsServiceClientBuilder.CreateClient<INetworkBVPSService>(
                    //    new ScsTcpEndPoint(Program.ServerIp, Program.ServerPort))
                    //)
                    var client = SslScsServiceClientBuilder.CreateSslClient<INetworkBVPSService>(
                        new ScsTcpEndPoint(Program.ServerIp, Program.ServerPort)
                        , Program.ServerPublicKey
                        , Program.ServerIp
                        , SslScsAuthMode.ServerAuth
                        , null
                        )
                        )
                {
                    client.Timeout = 10 * 1000; //timeout 1 minutes 
                    client.ConnectTimeout = 5 * 1000; //Time to connect // 5 seconds
                    client.Connect();
                    var details = new RequiredCommunicationDetails
                    {
                        DeviceID = Program.DeviceId,
                        TimeStamp = DateTime.Now,
                        ScanningDetails = RequiredDetailsHelper.GetRequiredScanningDetails()
                    };


                    var requiredDetails = new RequiredCommunicationDetailsPacket
                    {
                        DeviceID = Program.DeviceId,
                        CommunicationDetails = details
                    };
                    requiredDetails.PrepareForSending(Program.AuthKey);


                    var verResult = client.ServiceProxy.ScanHost(requiredDetails);


                    switch (verResult.ScanningResult)
                    {
                        case ScanningReturnResult.InvalidAuthentication:
                            LogHelper.LogMessageToAll(Program.FileLogger, null,
                                $"Scanning Refused by the server, due to ${ScanningReturnResult.InvalidAuthentication}, ID={Program.DeviceId}",
                                LogMsgType.Warning);
                            break;
                        case ScanningReturnResult.Error:
                            MessageBox.Show(@"UNKNOWN ERROR, try again later!");
                            break;
                        case ScanningReturnResult.Success:
                            LogHelper.LogMessageToAll(Program.FileLogger, null,
                                $"Scan result received succesfully for Device ID={Program.DeviceId} \n Received Response from Server {verResult.Message}",
                                LogMsgType.Debug);
                            GetResult(verResult.JobId);

                            break;
                        case ScanningReturnResult.ServerBusyJobRefused:
                            LogHelper.LogMessageToAll(Program.FileLogger, null,
                                $"Scanning Refused by the server, due to ${ScanningReturnResult.ServerBusyJobRefused}, ID={Program.DeviceId}",
                                LogMsgType.Warning);
                            break;
                        case ScanningReturnResult.StillScanning:
                            LogHelper.LogMessageToAll(Program.FileLogger, null,
                                $"Scanning in progress by the server, due to ${ScanningReturnResult.StillScanning}, ID={Program.DeviceId}",
                                LogMsgType.Warning);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"exception [most probably server is not running] " + ex);
                Program.FileLogger.Log(@"exception [most probably server is not running] " + ex.Message,
                    LogMsgType.Exception);
            }
            finally
            {
                EnableUI();
            }
            // rtb.AppendText($"Required {stopwatch.ElapsedMilliseconds}ms to finish");

        }


        private void BlockUI()
        {
            foreach (var textBox in Controls.OfType<TextBox>())
            {
                textBox.BeginInvoke((Action)delegate { textBox.Enabled = false; });
            }
            scanBtn.BeginInvoke((Action)delegate { scanBtn.Enabled = false; });
        }

        private void EnableUI()
        {
            foreach (var textBox in Controls.OfType<TextBox>())
            {
                textBox.BeginInvoke((Action)delegate { textBox.Enabled = true; });
            }
            scanBtn.BeginInvoke((Action)delegate
           {
               scanBtn.Enabled = true;
               scanBtn.Text = "Scan This Machine";
           });
        }
        private void GetResult(Guid verResultJobId)
        {
            bool gotIt = false;
            int waitTime = 1000;
            int cnt = 0;
            do
            {
                Thread.Sleep(waitTime);
                UpdateProgress(cnt++ % 2 == 0 ? $"Getting result..." : $"Please wait...");

                try
                {
                    using (var client = SslScsServiceClientBuilder.CreateSslClient<INetworkBVPSService>(
                        new ScsTcpEndPoint(Program.ServerIp, Program.ServerPort)
                        ,Program.ServerPublicKey
                        , Program.ServerIp
                        , SslScsAuthMode.ServerAuth
                        , null
                    ))
                    {
                        client.Timeout = 10 * 60 * 1000;//timeout 10 minutes 
                        var details = new RequiredCommunicationDetails
                        {
                            JobId = verResultJobId,
                            DeviceID = Program.DeviceId,
                            TimeStamp = DateTime.Now,
                            ScanningDetails = RequiredDetailsHelper.GetRequiredScanningDetails()
                        };


                        var requiredDetails = new RequiredCommunicationDetailsPacket
                        {
                            DeviceID = Program.DeviceId,
                            CommunicationDetails = details
                        };

                        requiredDetails.PrepareForSending(Program.AuthKey);

                        client.Connect();
                        var verResult = client.ServiceProxy.GetResult(requiredDetails);


                        switch (verResult.ScanningResult)
                        {
                            case ScanningReturnResult.InvalidAuthentication:
                                LogHelper.LogMessageToAll(Program.FileLogger, null, $"Scanning Refused by the server, due to ${ScanningReturnResult.InvalidAuthentication}, ID={Program.DeviceId}", LogMsgType.Warning);
                                break;
                            case ScanningReturnResult.Error:
                                MessageBox.Show(@"UNKNOWN ERROR, try again later!");
                                gotIt = true;
                                break;
                            case ScanningReturnResult.Success:
                                LogHelper.LogMessageToAll(Program.FileLogger, null, $"Scan result received succesfully for Device ID={Program.DeviceId} \n Received Response from Server {verResult.Message}", LogMsgType.Debug);
                                var report = ReportingHelper.GetCompleteReport(verResult.CompressedHTML);
                                var dial = new SaveFileDialog();
                                dial.FileName = "Report.html";
                                dial.Filter = "html files (*.html)|*.html";
                                BeginInvoke((Action)(() =>
                                {
                                    if (dial.ShowDialog() == DialogResult.OK)
                                    {
                                        File.WriteAllText(dial.FileName, report);
                                        LogHelper.LogMessageToAll(Program.FileLogger, null, $"User saved the report to {dial.FileName}", LogMsgType.Warning);
                                        Process.Start(dial.FileName);
                                    }
                                    else
                                    {
                                        LogHelper.LogMessageToAll(Program.FileLogger, null, $"User selected not to save the report", LogMsgType.Warning);

                                    }
                                }), null);


                                gotIt = true;
                                break;
                            case ScanningReturnResult.StillScanning:
                                waitTime *= 2;
                                break;
                            case ScanningReturnResult.ServerBusyJobRefused:
                                LogHelper.LogMessageToAll(Program.FileLogger, null, $"Scanning Refused by the server, due to ${ScanningReturnResult.ServerBusyJobRefused}, ID={Program.DeviceId}", LogMsgType.Warning);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"exception [most probably server is not running] " + ex);
                    Program.FileLogger.Log(@"exception [most probably server is not running] " + ex.Message,
                        LogMsgType.Exception);
                }


            } while (!gotIt);
            EnableUI();
        }

        //  MAKE THE BORDERLESS MOVABLE
        protected override void WndProc(ref Message m)
        {
            try
            {
                switch (m.Msg)
                {
                    case 0x84:
                        base.WndProc(ref m);
                        if ((int)m.Result == 0x1)
                            m.Result = (IntPtr)0x2;
                        return;
                }
                base.WndProc(ref m);
            }
            catch (Exception ex)
            {
                MessageBox.Show("\n IN [WndProc] \n Exception: \n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }
        protected override void OnResize(EventArgs e)
        {
            while (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
            }
            //base.OnResize(e);
        }
        private void scanBtn_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => { Scan(); });
        }
    }
}
