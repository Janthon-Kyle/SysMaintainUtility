using Sunny.UI;

namespace SysMaintainUtility
{
    public partial class Launcher : UIForm
    {
        public Launcher()
        {
            InitializeComponent();
        }

        readonly string UtilitiesHome = Application.StartupPath + "/MaintainUtilities/";
        private static void LaunchApp(string AppPath)
        {
            if (MaintainUtilityAPI.CheckAppPathIfExist(AppPath)==1)
            {
                MaintainUtilityAPI.AppLauncher(AppPath);
            }
            else
            {
                MessageBox.Show("启动失败！文件不存在，请确认工具箱完整性！","维护工具箱",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void BtLcWinNTSetup_Click(object sender, EventArgs e)
        {
            LaunchApp(UtilitiesHome+ "WinNTSetup/WinNTSetup.exe");
        }

        private void BtLcDiskGenius_Click(object sender, EventArgs e)
        {
            LaunchApp(UtilitiesHome + "DiskGenius/DiskGenius.exe");
        }

        private void BtLcBootICE_Click(object sender, EventArgs e)
        {
            LaunchApp(UtilitiesHome + "BootICE/BOOTICE.exe");
        }

        private void BtLcNTPWEdit_Click(object sender, EventArgs e)
        {
            LaunchApp(UtilitiesHome + "NTPWEdit/NTPWEdit.exe");
        }

        private void BtLcRegistryWorkshop_Click(object sender, EventArgs e)
        {
            LaunchApp(UtilitiesHome + "Registry Workshop/RegWorkshopX64.exe");
        }

        private void BtLcDISMPP_Click(object sender, EventArgs e)
        {
            LaunchApp(UtilitiesHome + "Dism++/Dism++x64.exe");
        }

        private void BtLcAIDA64_Click(object sender, EventArgs e)
        {
            LaunchApp(UtilitiesHome + "AIDA64 Extreme/aida64.exe");
        }

        private void BtLcDiskInfo_Click(object sender, EventArgs e)
        {
            LaunchApp(UtilitiesHome + "CrystalDiskInfo/DiskInfo64.exe");
        }

        private void BtLcDiskMark_Click(object sender, EventArgs e)
        {
            LaunchApp(UtilitiesHome + "CrystalDiskMark/DiskMark64.exe");
        }

        private void BtLcDiskScanner_Click(object sender, EventArgs e)
        {
            LaunchApp(UtilitiesHome + "Macrorit Disk Scanner/dm.st.exe");
        }

        private void BtLcMemTest_Click(object sender, EventArgs e)
        {
            LaunchApp(UtilitiesHome + "MemTest/MemTest.exe");
        }

        private void BtLcCPUZ_Click(object sender, EventArgs e)
        {
            LaunchApp(UtilitiesHome + "CPU-Z/cpuz.exe");
        }

        private void BtLcGPUZ_Click(object sender, EventArgs e)
        {
            LaunchApp(UtilitiesHome + "GPU-Z/GPU-Z.exe");
        }
    }
}