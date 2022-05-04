using Sunny.UI;

namespace SysMaintainUtility
{
    public partial class Launcher : UIForm
    {
        public Launcher()
        {
            InitializeComponent();
        }

        //定义工具主目录常量
        readonly string UtilitiesHome = Application.StartupPath + @"\MaintainUtilities\";
        //定义打印机驱动主目录常量
        readonly string PrinterDrvHome = Application.StartupPath + @"\PrinterDrv\";

        /// <summary>
        /// 异步任务运行情况,
        /// 运行中 = true,
        /// 未运行 = false.
        /// </summary>
        bool asyncWorkFinished = true;

        /// <summary>
        /// 被选中的柯美打印机型号,
        /// BHC226 = 1,
        /// BHC266 = 2,
        /// BHC368 = 3.
        /// </summary>
        int chkKMPrtModel = 0;

        /// <summary>
        /// 柯美打印机驱动安装信息
        /// </summary>
        private struct KMPrtDrvInstInfo
        {
            internal string displayName;
            internal string driverInf;
            internal string driverStandardName;
        };

        /// <summary>
        /// 柯美打印机驱动安装信息路径
        /// </summary>
        private struct KMDrvInfPathInfo
        {
            internal string driverInfPath;
        };

        /// <summary>
        /// 启动工具（检查工具是否存在，若存在则启动，不存在则报错）
        /// </summary>
        /// <param name="appPath">工具目录</param>
        private static void LaunchApp(string appPath)
        {
            if (MaintainUtility.CheckAppPathIfExist(appPath))
            {
                MaintainUtility.AppLauncher(appPath);
            }
            else
            {
                MessageBox.Show("启动失败！文件不存在，请确认工具箱完整性！", "维护工具箱", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            //定义柯美打印机驱动安装信息路径
            KMDrvInfPathInfo KMBHC226InfPath = new();
            KMBHC226InfPath.driverInfPath = PrinterDrvHome + @"KONICA MINOLTA\BHC266PCL6Winx64\KOAXLJ__.INF";
            KMDrvInfPathInfo KMBHC266InfPath = new();
            KMBHC266InfPath.driverInfPath = PrinterDrvHome + @"KONICA MINOLTA\BHC266PCL6Winx64\KOAXLJ__.INF";
            KMDrvInfPathInfo KMBHC368InfPath = new();
            KMBHC368InfPath.driverInfPath = PrinterDrvHome + @"KONICA MINOLTA\BHC368PCL6Winx64\KOAXPJ__.INF";

            //判断是否选择打印机型号
            if (chkKMPrtModel == 0)
            {
                MessageBox.Show("请选择要安装的打印机型号！", "KONICA MINOLTA 打印机驱动安装", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                //判断是否填写端口名称和端口地址
                if (string.IsNullOrEmpty(tbKMPortName.Text) || string.IsNullOrEmpty(tbKMPortAddress.Text))
                {
                    MessageBox.Show("请检查端口名称和端口地址是否有误！", "KONICA MINOLTA 打印机驱动安装", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //显示安装状态
                    rbGroupKMModel.Enabled = false;
                    KMDrvInstProgressIndicator.Visible = true;
                    lbKMDrvInstStatus.Text = "正在安装...";
                    //添加打印端口
                    MaintainUtility.AddPrinterPort(tbKMPortName.Text, tbKMPortAddress.Text);
                    //重启打印服务
                    MaintainUtility.ReloadPrintService();
                    //调用系统接口进行驱动安装
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

        //驱动安装失败提示
        private void DrvInstallFailedInfo()
        {
            MessageBox.Show("请检查驱动目录是否完整！", "KONICA MINOLTA 打印机驱动安装", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            KMDrvInstProgressIndicator.Visible = false;
            lbKMDrvInstStatus.Text = null;
            MessageBox.Show("安装失败", "KONICA MINOLTA 打印机驱动安装", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //驱动安装成功提示
        private void DrvInstallSucceedInfo()
        {
            KMDrvInstProgressIndicator.Visible = false;
            lbKMDrvInstStatus.Text = null;
            rbGroupKMModel.Enabled = true;
            MessageBox.Show("安装成功", "KONICA MINOLTA 打印机驱动安装", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        //后台安装BHC226驱动
        private void BgInstKMDrvC226_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //定义打印机对应型号信息
            KMPrtDrvInstInfo KMBHC226 = new();
            KMBHC226.displayName = "KONICA MINOLTA bizhub C226 PCL";
            KMBHC226.driverInf = @"KONICA MINOLTA\BHC266PCL6Winx64\KOAXLJ__.INF";
            KMBHC226.driverStandardName = "KONICA MINOLTA C266SeriesPCL";
            MaintainUtility.PrinterDrvInstall(KMBHC226.displayName, PrinterDrvHome + KMBHC226.driverInf, tbKMPortName.Text, KMBHC226.driverStandardName);
            asyncWorkFinished = true;
        }

        //后台安装BHC266驱动
        private void BgInstKMDrvC266_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //定义打印机对应型号信息
            KMPrtDrvInstInfo KMBHC266 = new();
            KMBHC266.displayName = "KONICA MINOLTA bizhub C266 PCL";
            KMBHC266.driverInf = @"KONICA MINOLTA\BHC266PCL6Winx64\KOAXLJ__.INF";
            KMBHC266.driverStandardName = "KONICA MINOLTA C266SeriesPCL";
            MaintainUtility.PrinterDrvInstall(KMBHC266.displayName, PrinterDrvHome + KMBHC266.driverInf, tbKMPortName.Text, KMBHC266.driverStandardName);
            asyncWorkFinished = true;
        }

        //后台安装BHC368驱动
        private void BgInstKMDrvC368_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //定义打印机对应型号信息
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