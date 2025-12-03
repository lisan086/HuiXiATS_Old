using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Common.JiChuLei
{
    /// <summary>
    /// 清理缓存用的
    /// </summary>
    public static class QingLiNeiCunLei
    {
        [DllImport("psapi.dll")]
        private static extern int EmptyWorkingSet(IntPtr hwProc); //清理内存相关
        /// <summary>
        /// 清理
        /// </summary>
        public static void QingLi()
        {

            //清理内存
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                //以下系统进程没有权限，所以跳过，防止出错影响效率。  
                if ((process.ProcessName == "System") && (process.ProcessName == "Idle"))
                    continue;
                try
                {
                    EmptyWorkingSet(process.Handle);
                }
                catch
                {

                }
            }
        }

        /// <summary>
        /// 清理缓存
        /// </summary>
        public static void QingLiHuanCun()
        {
            try
            {
                ManagementClass cimobject1 = new ManagementClass("Win32_PhysicalMemory");
                ManagementObjectCollection moc1 = cimobject1.GetInstances();
                double available = 0, capacity = 0;
                foreach (ManagementObject mo1 in moc1)
                {
                    capacity += ((Math.Round(Int64.Parse(mo1.Properties["Capacity"].Value.ToString()) / 1024 / 1024 / 1024.0, 1)));
                }
                moc1.Dispose();
                cimobject1.Dispose();


                //获取内存可用大小
                ManagementClass cimobject2 = new ManagementClass("Win32_PerfFormattedData_PerfOS_Memory");
                ManagementObjectCollection moc2 = cimobject2.GetInstances();
                foreach (ManagementObject mo2 in moc2)
                {
                    available += ((Math.Round(Int64.Parse(mo2.Properties["AvailableMBytes"].Value.ToString()) / 1024.0, 1)));

                }
                moc2.Dispose();
                cimobject2.Dispose();
                if (capacity <= 0)
                {
                    capacity = 100;
                }
                if (available <= 0)
                {
                    available = 100;
                }
                double hansheng = capacity - available;
                double baifenshu = hansheng / capacity;
                if (baifenshu > 0.85)
                {
                    QingLi();
                }
            }
            catch
            {

            }



        }
    }
}
