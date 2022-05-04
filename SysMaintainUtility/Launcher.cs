using Sunny.UI;

namespace SysMaintainUtility
{
    public partial class Launcher : UIForm
    {
        public Launcher()
        {
            InitializeComponent();
        }

        //���幤����Ŀ¼����
        readonly string UtilitiesHome = Application.StartupPath + @"\MaintainUtilities\";
        //�����ӡ��������Ŀ¼����
        readonly string PrinterDrvHome = Application.StartupPath + @"\PrinterDrv\";

        /// <summary>
        /// �첽�����������,
        /// ������ = true,
        /// δ���� = false.
        /// </summary>
        bool asyncWorkFinished = true;

        /// <summary>
        /// ��ѡ�еĿ�����ӡ���ͺ�,
        /// BHC226 = 1,
        /// BHC266 = 2,
        /// BHC368 = 3.
        /// </summary>
        int chkKMPrtModel = 0;

        /// <summary>
        /// ������ӡ��������װ��Ϣ
        /// </summary>
        private struct KMPrtDrvInstInfo
        {
            internal string displayName;
            internal string driverInf;
            internal string driverStandardName;
        };

        /// <summary>
        /// ������ӡ��������װ��Ϣ·��
        /// </summary>
        private struct KMDrvInfPathInfo
        {
            internal string driverInfPath;
        };

        /// <summary>
        /// �������ߣ���鹤���Ƿ���ڣ����������������������򱨴�
        /// </summary>
        /// <param name="appPath">����Ŀ¼</param>
        private static void LaunchApp(string appPath)
        {
            if (MaintainUtility.CheckAppPathIfExist(appPath))
            {
                MaintainUtility.AppLauncher(appPath);
            }
            else
            {
                MessageBox.Show("����ʧ�ܣ��ļ������ڣ���ȷ�Ϲ����������ԣ�", "ά��������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtLcWinNTSetup_Click(object sender, EventArgs e)
        {
            LaunchApp(UtilitiesHome + @"WinNTSetup\WinNTSetup.exe");
        }

        private void BtLcDiskGenius_Click(object sender, EventArgs e)
        {
            LaunchApp(UtilitiesHome + @"DiskGenius\DiskGenius.exe");
        }

        private void BtLcBootICE_Click(object sender, EventArgs e)
        {
            LaunchApp(UtilitiesHome + @"BootICE\BOOTICE.exe");
        }

        private void BtLcNTPWEdit_Click(object sender, EventArgs e)
        {
            LaunchApp(UtilitiesHome + @"NTPWEdit\NTPWEdit.exe");
        }

        private void BtLcRegistryWorkshop_Click(object sender, EventArgs e)
        {
            LaunchApp(UtilitiesHome + @"Registry Workshop\RegWorkshopX64.exe");
        }

        private void BtLcDISMPP_Click(object sender, EventArgs e)
        {
            LaunchApp(UtilitiesHome + @"Dism++\Dism++x64.exe");
        }

        private void BtLcAIDA64_Click(object sender, EventArgs e)
        {
            LaunchApp(UtilitiesHome + @"AIDA64 Extreme\aida64.exe");
        }

        private void BtLcDiskInfo_Click(object sender, EventArgs e)
        {
            LaunchApp(UtilitiesHome + @"CrystalDiskInfo\DiskInfo64.exe");
        }

        private void BtLcDiskMark_Click(object sender, EventArgs e)
        {
            LaunchApp(UtilitiesHome + @"CrystalDiskMark\DiskMark64.exe");
        }

        private void BtLcDiskScanner_Click(object sender, EventArgs e)
        {
            LaunchApp(UtilitiesHome + @"Macrorit Disk Scanner\dm.st.exe");
        }

        private void BtLcMemTest_Click(object sender, EventArgs e)
        {
            LaunchApp(UtilitiesHome + @"MemTest\MemTest.exe");
        }

        private void BtLcCPUZ_Click(object sender, EventArgs e)
        {
            LaunchApp(UtilitiesHome + @"CPU-Z\cpuz.exe");
        }

        private void BtLcGPUZ_Click(object sender, EventArgs e)
        {
            LaunchApp(UtilitiesHome + @"GPU-Z\GPU-Z.exe");
        }

        private void BtLcUninstaller_Click(object sender, EventArgs e)
        {
            LaunchApp(UtilitiesHome + @"HiBitUninstaller\HiBitUninstaller.exe");
        }

        private void BtLcStartupManager_Click(object sender, EventArgs e)
        {
            LaunchApp(UtilitiesHome + @"HiBitStartupManager\HiBitStartupManager.exe");
        }

        private void BtKMDrvInst_Click(object sender, EventArgs e)
        {
            //���������ӡ��������װ��Ϣ·��
            KMDrvInfPathInfo KMBHC226InfPath = new();
            KMBHC226InfPath.driverInfPath = PrinterDrvHome + @"KONICA MINOLTA\BHC266PCL6Winx64\KOAXLJ__.INF";
            KMDrvInfPathInfo KMBHC266InfPath = new();
            KMBHC266InfPath.driverInfPath = PrinterDrvHome + @"KONICA MINOLTA\BHC266PCL6Winx64\KOAXLJ__.INF";
            KMDrvInfPathInfo KMBHC368InfPath = new();
            KMBHC368InfPath.driverInfPath = PrinterDrvHome + @"KONICA MINOLTA\BHC368PCL6Winx64\KOAXPJ__.INF";

            //�ж��Ƿ�ѡ���ӡ���ͺ�
            if (chkKMPrtModel == 0)
            {
                MessageBox.Show("��ѡ��Ҫ��װ�Ĵ�ӡ���ͺţ�", "KONICA MINOLTA ��ӡ��������װ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //�ж��Ƿ���д�˿����ƺͶ˿ڵ�ַ
                if (string.IsNullOrEmpty(tbKMPortName.Text) || string.IsNullOrEmpty(tbKMPortAddress.Text))
                {
                    MessageBox.Show("����˿����ƺͶ˿ڵ�ַ�Ƿ�����", "KONICA MINOLTA ��ӡ��������װ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //��ʾ��װ״̬
                    rbGroupKMModel.Enabled = false;
                    KMDrvInstProgressIndicator.Visible = true;
                    lbKMDrvInstStatus.Text = "���ڰ�װ...";
                    //��Ӵ�ӡ�˿�
                    MaintainUtility.AddPrinterPort(tbKMPortName.Text, tbKMPortAddress.Text);
                    //������ӡ����
                    MaintainUtility.ReloadPrintService();
                    //����ϵͳ�ӿڽ���������װ
                    if (chkKMPrtModel == 1)
                    {
                        if (MaintainUtility.CheckDrvInfIfExist(KMBHC226InfPath.driverInfPath))
                        {
                            asyncWorkFinished = false;
                            bgInstKMDrvC226.RunWorkerAsync();
                            timerWorkStatus.Start();
                        }
                        else
                        {
                            DrvInstallFailedInfo();
                        }
                    }
                    else if (chkKMPrtModel == 2)
                    {
                        if (MaintainUtility.CheckDrvInfIfExist(KMBHC266InfPath.driverInfPath))
                        {
                            asyncWorkFinished = false;
                            bgInstKMDrvC266.RunWorkerAsync();
                            timerWorkStatus.Start();
                        }
                        else
                        {
                            DrvInstallFailedInfo();
                        }
                    }
                    else if (chkKMPrtModel == 3)
                    {
                        if (MaintainUtility.CheckDrvInfIfExist(KMBHC368InfPath.driverInfPath))
                        {
                            asyncWorkFinished = false;
                            bgInstKMDrvC368.RunWorkerAsync();
                            timerWorkStatus.Start();
                        }
                        else
                        {
                            DrvInstallFailedInfo();
                        }
                    }
                }
            }
        }

        //������װʧ����ʾ
        private void DrvInstallFailedInfo()
        {
            MessageBox.Show("��������Ŀ¼�Ƿ�������", "KONICA MINOLTA ��ӡ��������װ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            KMDrvInstProgressIndicator.Visible = false;
            lbKMDrvInstStatus.Text = null;
            MessageBox.Show("��װʧ��", "KONICA MINOLTA ��ӡ��������װ", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //������װ�ɹ���ʾ
        private void DrvInstallSucceedInfo()
        {
            KMDrvInstProgressIndicator.Visible = false;
            lbKMDrvInstStatus.Text = null;
            rbGroupKMModel.Enabled = true;
            MessageBox.Show("��װ�ɹ�", "KONICA MINOLTA ��ӡ��������װ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RbKMC226_CheckedChanged(object sender, EventArgs e)
        {
            if (rbKMC226.Checked)
            {
                chkKMPrtModel = 1;
            }
        }

        private void RbKMC266_CheckedChanged(object sender, EventArgs e)
        {
            if (rbKMC266.Checked)
            {
                chkKMPrtModel = 2;
            }
        }

        private void RbKMC368_CheckedChanged(object sender, EventArgs e)
        {
            if (rbKMC368.Checked)
            {
                chkKMPrtModel = 3;
            }
        }

        //��̨��װBHC226����
        private void BgInstKMDrvC226_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //�����ӡ����Ӧ�ͺ���Ϣ
            KMPrtDrvInstInfo KMBHC226 = new();
            KMBHC226.displayName = "KONICA MINOLTA bizhub C226 PCL";
            KMBHC226.driverInf = @"KONICA MINOLTA\BHC266PCL6Winx64\KOAXLJ__.INF";
            KMBHC226.driverStandardName = "KONICA MINOLTA C266SeriesPCL";
            MaintainUtility.PrinterDrvInstall(KMBHC226.displayName, PrinterDrvHome + KMBHC226.driverInf, tbKMPortName.Text, KMBHC226.driverStandardName);
            asyncWorkFinished = true;
        }

        //��̨��װBHC266����
        private void BgInstKMDrvC266_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //�����ӡ����Ӧ�ͺ���Ϣ
            KMPrtDrvInstInfo KMBHC266 = new();
            KMBHC266.displayName = "KONICA MINOLTA bizhub C266 PCL";
            KMBHC266.driverInf = @"KONICA MINOLTA\BHC266PCL6Winx64\KOAXLJ__.INF";
            KMBHC266.driverStandardName = "KONICA MINOLTA C266SeriesPCL";
            MaintainUtility.PrinterDrvInstall(KMBHC266.displayName, PrinterDrvHome + KMBHC266.driverInf, tbKMPortName.Text, KMBHC266.driverStandardName);
            asyncWorkFinished = true;
        }

        //��̨��װBHC368����
        private void BgInstKMDrvC368_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //�����ӡ����Ӧ�ͺ���Ϣ
            KMPrtDrvInstInfo KMBHC368 = new();
            KMBHC368.displayName = "KONICA MINOLTA bizhub C368 PCL";
            KMBHC368.driverInf = @"KONICA MINOLTA\BHC368PCL6Winx64\KOAXPJ__.INF";
            KMBHC368.driverStandardName = "KONICA MINOLTA C368SeriesPCL";
            MaintainUtility.PrinterDrvInstall(KMBHC368.displayName, PrinterDrvHome + KMBHC368.driverInf, tbKMPortName.Text, KMBHC368.driverStandardName);
            asyncWorkFinished = true;
        }

        private void TimerWorkStatus_Tick(object sender, EventArgs e)
        {
            if (asyncWorkFinished)
            {
                timerWorkStatus.Stop();
                DrvInstallSucceedInfo();
            }
        }
    }
}