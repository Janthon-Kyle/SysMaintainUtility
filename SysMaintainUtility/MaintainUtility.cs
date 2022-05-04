using Microsoft.Win32;
using System.Diagnostics;
using System.ServiceProcess;

namespace SysMaintainUtility
{
    internal class MaintainUtility
    {
        internal static void AppLauncher(string appPath)
        {
            Process.Start(appPath);
        }

        /// <summary>
        /// 检查应用程序路径是否存在
        /// </summary>
        /// <param name="appPath">应用程序路径</param>
        /// <returns>若存在则返回true，不存在则返回false。</returns>
        internal static bool CheckAppPathIfExist(string appPath)
        {
            if (File.Exists(appPath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 检查驱动文件是否存在
        /// </summary>
        /// <param name="driverFolder">驱动所在目录</param>
        /// <returns>若存在则返回true，不存在则返回false。</returns>
        internal static bool CheckDrvInfIfExist(string infPath)
        {
            if (File.Exists(infPath))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 添加打印机IPP端口
        /// </summary>
        /// <param name="portName">端口名称</param>
        /// <param name="portAddress">端口地址</param>
        internal static int AddPrinterPort(string portName, string portAddress)
        {
            //设置IPP端口监听
            RegistryKey IPPPortsSetting;
            IPPPortsSetting = Registry.LocalMachine.CreateSubKey(@"SYSTEM\CurrentControlSet\Control\Print\Monitors\Standard TCP/IP Port\Ports");
            IPPPortsSetting.SetValue("LprAckTimeout", 180, RegistryValueKind.DWord);
            IPPPortsSetting.SetValue("StatusUpdateEnabled", 1, RegistryValueKind.DWord);
            IPPPortsSetting.SetValue("StatusUpdateInterval", 10, RegistryValueKind.DWord);
            IPPPortsSetting.Close();
            //设置一个新的IPP端口
            RegistryKey PrinterPortKey;
            PrinterPortKey = Registry.LocalMachine.CreateSubKey(@"SYSTEM\CurrentControlSet\Control\Print\Monitors\Standard TCP/IP Port\Ports\" + portName);
            PrinterPortKey.SetValue("Protocol", 1, RegistryValueKind.DWord);
            PrinterPortKey.SetValue("Version", 2, RegistryValueKind.DWord);
            PrinterPortKey.SetValue("HostName", "", RegistryValueKind.String);
            PrinterPortKey.SetValue("IPAddress", portAddress, RegistryValueKind.String);
            PrinterPortKey.SetValue("HWAddress", "", RegistryValueKind.String);
            PrinterPortKey.SetValue("PortNumber", 9100, RegistryValueKind.DWord);
            PrinterPortKey.SetValue("SNMP Community", "internal", RegistryValueKind.String);
            PrinterPortKey.SetValue("SNMP Enabled", 1, RegistryValueKind.DWord);
            PrinterPortKey.SetValue("SNMP Index", 1, RegistryValueKind.DWord);
            PrinterPortKey.SetValue("PortMonMibPortIndex", 0, RegistryValueKind.DWord);
            PrinterPortKey.Close();
            return 0;
        }

        /// <summary>
        /// 重新加载 Print Spooler 服务
        /// </summary>
        internal static int ReloadPrintService()
        {
            ServiceController sc = new("Spooler");
            //检查Print Spooler服务状态
            //如果已经启动则停止服务，否则启动服务
            if (!sc.Equals(ServiceControllerStatus.Running))
            {
                sc.Stop();
                //等待服务成功停止，然后再启动服务
                sc.WaitForStatus(ServiceControllerStatus.Stopped);
                sc.Start();
            }
            else
            {
                sc.WaitForStatus(ServiceControllerStatus.Stopped);
                sc.Start();
                sc.Refresh();
            }
            sc.WaitForStatus(ServiceControllerStatus.Running);
            return 0;
        }

        /// <summary>
        /// 打印机驱动程序安装
        /// </summary>
        /// <param name="displayName">打印机显示名称</param>
        /// <param name="driverINF">驱动INF文件路径</param>
        /// <param name="portName">TCP/IP端口名称</param>
        /// <param name="driverStandardName">驱动标准名称</param>
        internal static void PrinterDrvInstall(string displayName, string driverINF, string portName, string driverStandardName)
        {
            ProcessStartInfo startInfo = new("rundll32.exe")
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                Arguments = @" printui.dll,PrintUIEntry /if /b " + "\"" + displayName + "\"" + @" /f " + "\"" + @driverINF + "\"" + @" /r " + "\"" + portName + "\"" + " /m " + "\"" + driverStandardName + "\"" + " /z",
            };
            Process procInstall = new();
            procInstall.StartInfo = startInfo;
            procInstall.Start();
            procInstall.WaitForExit();
            procInstall.Close();
        }
    }
}
