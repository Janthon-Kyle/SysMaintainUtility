using System.Diagnostics;

namespace SysMaintainUtility
{
    internal class MaintainUtilityAPI
    {
        public static void AppLauncher(string AppPath)
        {
            Process.Start(AppPath);
        }

        /// <summary>
        /// 检查应用程序路径是否存在，若存在则返回1，不存在则返回0。
        /// </summary>
        /// <param name="appPath">应用程序路径</param>
        /// <returns></returns>
        public static int CheckAppPathIfExist(string AppPath)
        {
            if (File.Exists(AppPath))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

    }
}
